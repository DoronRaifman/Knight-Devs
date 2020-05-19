
from flask import Flask, request, Response, render_template, jsonify

app = Flask(__name__)


@app.route('/')
def main_page():
    return render_template('index.html')


@app.route('/Page1')
def page1():
    return render_template('page1.html')


if __name__ == '__main__':
    app.run()


