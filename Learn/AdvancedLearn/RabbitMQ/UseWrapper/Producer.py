import time

from Learn.AdvancedLearn.RabbitMQ.UseWrapper.RabbitInstance import RabbitInstance


class Producer:
    def do_work(self):
        print("Start Producer")
        queue_name = RabbitInstance.default_queue_name
        RabbitInstance.connect()
        RabbitInstance.declare_queue(queue_name)
        for i in range(1, 100+1):
            message = {'message': f'Message {i}', }
            print(f"send message {i}")
            RabbitInstance.send_message(queue_name, message)
            # time.sleep(5)
        RabbitInstance.disconnect()
        print("Producer terminated")


if __name__ == '__main__':
    Producer().do_work()

