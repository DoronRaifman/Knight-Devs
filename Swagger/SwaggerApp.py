from flask import Flask, render_template, jsonify
from SwaggerWorker import SwaggerWorker


class SwaggerApp:
    worker = SwaggerWorker()

    def __init__(self):
        pass


app = Flask(__name__)


@app.route('/')
def index():
    return render_template('home.html')


# /book_details
# sample call example:
# http://127.0.0.1:5000/book_details/1234567890/HarryPotter/2009
@app.route('/book_details/<string:book_id>/<string:book_name>/<int:book_year>', methods=['GET'])
def book_details(book_id: str, book_name: str, book_year: int) -> str:
    print(f'/book_details/{book_id}/{book_name}/{book_year}')
    result = SwaggerApp.worker.book_details(book_id, book_name, book_year)
    print(f'\tresult: {result}')
    return jsonify(result)


if __name__ == '__main__':
    print(f"start serving Swagger Site")
    app.run()

