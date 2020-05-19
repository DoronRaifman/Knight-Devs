
import time
import socket
import threading


class Server:
    def __init__(self):
        self.server_socket = None
        self.base_isToRun = False
        self.accept_clients_is_running = False
        self.read_clients_is_running = False
        self._data_lock = threading.Lock()
        self.clients = []

    def init(self, ip='localhost', port=10000):
        self.server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.server_socket.bind((ip, port))
        self.server_socket.listen(5)    # become a server socket, maximum 5 connections
        self.base_isToRun = True
        t = threading.Thread(target=self.run_server_accept_clients)
        t.start()

    def cleanup(self):
        self.base_isToRun = False
        for client_index, client in enumerate(self.clients):
            client.close()
        self.clients = []
        while self.accept_clients_is_running or self.read_clients_is_running:
            pass

    def run_server_accept_clients(self):
        self.accept_clients_is_running = True
        while self.base_isToRun:
            connection, address = self.server_socket.accept()
            self._data_lock.acquire()
            client_index = len(self.clients)
            print(f"Server accepted Client {client_index}")
            self.clients.append(connection)
            t = threading.Thread(target=self.run_server_read_client(client_index))
            t.start()
            self._data_lock.release()
            time.sleep(0.1)
        self.accept_clients_is_running = False

    def run_server_read_client(self, client_index):
        self.accept_clients_is_running = True
        client = self.clients[client_index]
        print(f"Client {client_index} Opened")
        while self.base_isToRun:
            buff = client.recv(64)
            if len(buff) > 0:
                print(f"Got from client {client_index}: {buff}")
            time.sleep(0.1)
        print(f"Client {client_index} Closed")
        self.accept_clients_is_running = False


class Client:
    def __init__(self):
        self.socket = None

    def init(self, ip='localhost', port=10000):
        print(f"Client: init")
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.socket.connect((ip, port))

    def cleanup(self):
        if self.socket is not None:
            self.socket.close()
        self.socket = None

    def send_message(self, line):
        self.socket.send(bytes(line, encoding="utf-8"))


if __name__ == '__main__':
    print("==> App started")
    print("==> Start server")
    server = Server()
    server.init()

    print("==> Start clients")
    clients = []
    for i in range(1):
        client = Client()
        clients.append(client)
        client.init()

    time.sleep(1)

    print("==> Send messages")
    for i in range(5):
        for client_index, client in enumerate(clients):
            client.send_message(f"({client_index}, Message {i+1}) ")
        time.sleep(0.1)
    time.sleep(1)

    print("==> Close clients")
    for client in clients:
        client.cleanup()

    print("==> Close server")
    server.cleanup()
    print("==> App terminated")


