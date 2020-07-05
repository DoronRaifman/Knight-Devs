import time

from Learn.AdvancedLearn.RabbitMQ.UseWrapper.RabbitInstance import RabbitInstance


class Consumer:
    def do_work(self):
        queue_name = RabbitInstance.default_queue_name
        RabbitInstance.connect()
        RabbitInstance.declare_queue(queue_name)
        print("Start Consumer")
        RabbitInstance.start_consuming(queue_name, self.callback_func, prefetch_count=1)
        # next line will not be callled because prev line blocking forever
        RabbitInstance.disconnect()

    @classmethod
    def callback_func(cls, channel, method, properties, body):
        print(f" [x] Received {body}")
        # Todo: do your stuff - consume message
        time.sleep(0.1)   # simulate work is done
        print(" [x] Done processing")


if __name__ == '__main__':
    Consumer().do_work()

