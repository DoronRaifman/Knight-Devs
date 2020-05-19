
from flask import Flask, request, Response, render_template, jsonify

app = Flask(__name__)


@app.route('/')
def hello_world():
    rows = []
    for i in range(5):
        # row = [f"Field {i}" for i in range(3)]
        row = {'ID': i, 'Name': f"Field {i}", 'Order id': 5}
        rows.append(row)
    return render_template('index.html', rows=rows)


if __name__ == '__main__':
    app.run()


