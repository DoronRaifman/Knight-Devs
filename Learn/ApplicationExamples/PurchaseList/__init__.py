import os
import os
import socket
import logging
import warnings

from flask import Flask, request, render_template, jsonify, send_from_directory, url_for

# import Learn.ApplicationExamples.PurchaseList.Core.LogHelper
from Learn.ApplicationExamples.PurchaseList.Core.Enums import *
from Learn.ApplicationExamples.PurchaseList.Core.PurchaseList import *


app = Flask(__name__)


class Main:
    logger = None
    purchase_list = PurchaseList()
    cur_papa = 0

    @classmethod
    def configure_log(cls, use_console=True, name="App", folder='log', logger_name=None,
                      level=logging.DEBUG,
                      message_format='%(asctime)s %(levelname)s %(filename)s:%(lineno)d %(funcName)s(): %(message)s'):
        # date_format = "%y-%m-%d %H:%M:%S"
        date_format = "%H:%M:%S"
        formatter = logging.Formatter(fmt=message_format, datefmt=date_format)
        os.makedirs(folder, exist_ok=True)
        log_file = os.path.join(folder, '%s.log' % name)
        # file_handler = RotatingFileHandler(log_file, mode='w', encoding='utf8', maxBytes=1000000, backupCount=3)
        file_handler = logging.FileHandler(log_file, mode='w', encoding='utf8')
        file_handler.setLevel(level)  # TODO from config
        handlers = [file_handler]
        if use_console:
            console_handler = logging.StreamHandler()
            handlers.append(console_handler)
            console_handler.setLevel(level)
        logger = logging.getLogger() if logger_name is None else logging.getLogger(logger_name)
        logger.setLevel(level)  # TODO from config
        for h in handlers:
            h.setFormatter(formatter)
        logger.handlers = handlers
        # logger.addHandler(h)
        return logger

    @classmethod
    def start_work(cls):
        cls.logger = cls.configure_log(name='default', logger_name='default')
        warnings.simplefilter("ignore")

        host_name = socket.gethostname()
        real_ip = socket.gethostbyname(host_name)
        # real_ip = "127.0.0.1"
        port = 25347
        url = f"http://{real_ip}:{port}"

        # start browser
        # threading.Timer(1.25, lambda: webbrowser.open(url)).start()
        cls.logger.debug(f"start serving recalc on url: {url}")
        # logging.getLogger("werkzeug").setLevel(logging.ERROR)
        app.run(host=real_ip, port=port)
        # app.run()
        # app.run(host=ip, port=port, debug=False)
        # app.run(port=port)


@app.route('/')
def index():
    Main.purchase_list.connect()
    items_list = Main.purchase_list.get_item_siblings(papa_id=Main.cur_papa)
    items = Main.purchase_list.item_list_to_dict_list(items_list)
    if Main.cur_papa != 0:
        papa = Main.purchase_list.find_item_by_id(Main.cur_papa)
        papa_name = papa.item_name
    else:
        papa_name = 'Top level'
    Main.purchase_list.disconnect()
    return render_template('index.html', papa_name=papa_name, items=items)


@app.route('/version', methods=['GET'])
def get_version():
    return f"<h3> {SoftwareVersion.version_name}</h3>"


@app.route('/goto', methods=['GET'])
def goto():
    item_id = int(request.args.get('item_id'))
    Main.cur_papa = item_id
    Main.logger.debug(f'goto {item_id}')
    return jsonify("OK")


@app.route('/goup', methods=['GET'])
def goup():
    if Main.cur_papa != 0:
        Main.purchase_list.connect()
        papa = Main.purchase_list.find_item_by_id(Main.cur_papa)
        Main.purchase_list.disconnect()
        Main.logger.debug(f'goup to {papa.papa_id}')
        Main.cur_papa = papa.papa_id
    else:
        Main.logger.debug(f'goup - already at root')
    return jsonify("OK")


@app.route('/favicon.ico')
def favicon():
    # app.add_url_rule('/favicon.ico', redirect_to=url_for('static', filename='favicon.ico'))
    return send_from_directory(os.path.join(app.root_path, 'static'), 'favicon.ico')


if __name__ == '__main__':
    Main.start_work()




