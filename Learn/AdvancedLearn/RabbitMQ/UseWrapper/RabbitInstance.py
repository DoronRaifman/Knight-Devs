import json
import sys
import time

import pika

"""
rabbit terminology

Producer: send messages to be processed (consumed)
Consumer (worker): consume messages, and ack processing (thus confirm message delete from Q) 


"""


class RabbitInstance:
    connection:pika.BlockingConnection = None
    channel: pika.BlockingConnection.channel = None
    default_queue_name = 'task_queue'
    callback_func = None

    @classmethod
    def connect(cls):
        cls.connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost'))
        cls.channel = cls.connection.channel()

    @classmethod
    def disconnect(cls):
        cls.connection.close()

    @classmethod
    def declare_queue(cls, queue_name='task_queue'):
        cls.routing_key = queue_name
        cls.channel.queue_declare(queue=cls.routing_key, durable=True)

    @classmethod
    def send_message(cls, routing_key, message):
        message_json = json.dumps(message, indent=4)
        cls.channel.basic_publish(
            exchange='',
            routing_key=routing_key,
            body=message_json,
            properties=pika.BasicProperties(
                delivery_mode = 2,  # make message persistent
            ))

    @classmethod
    def start_consuming(cls, routing_key, callback_func, prefetch_count=1):
        """
        start_consuming
        Blocking forever
        :param callback_func: def callback(channel, method, properties, body):
        :param prefetch_count:
        :return: does not return - blocking
        """
        cls.channel.basic_qos(prefetch_count=prefetch_count)
        cls.callback_func = callback_func
        cls.channel.basic_consume(queue=routing_key, on_message_callback=cls.callback_func_wrapper)
        cls.channel.start_consuming()

    @classmethod
    def _ack_message_handled(cls, method):
        """
        ack_message_handled
        will delete message from Q
        :param method:
        :return:
        """
        cls.channel.basic_ack(delivery_tag=method.delivery_tag)

    @classmethod
    def nack_message_handled(cls, method):
        """
        nack_message_handled
        will delete message from Q
        :param method:
        :return:
        """
        cls.channel.basic_nack(delivery_tag=method.delivery_tag)

    @classmethod
    def callback_func_wrapper(cls, channel, method, properties, json_body):
        body = json.loads(json_body)
        cls.callback_func(channel, method, properties, body)
        cls._ack_message_handled(method)

    @classmethod
    def callback_func_example(cls, channel, method, properties, body):
        """
        Example callback_func
        :param method:
        :param properties:
        :param body:
        :return:
        """
        print(f" [x] Received {body}")
        # Todo: do your stuff - consume message
        time.sleep(1)   # simulate work is done
        print(" [x] Done processing")

