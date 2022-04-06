import json
import os
import yaml
import requests


class SwaggerWorker:
    def __init__(self):
        self.book_result = {
            'book_id': '1234567890', 'book_name': 'HarryPotter', 'book_year': 2009,
        }
        self.books = {
            '1234567890': self.book_result,
        }

    def book_details(self, book_id: str, book_name: str, book_year: int) -> dict:
        if book_id in self.books:
            book = self.books[book_id]
            if book['book_name'] == book_name and book['book_year'] == book_year:
                result = book
            else:
                result = {'result': f'book name or year is wrong'}
        else:
            result = {'result': 'book not found'}
        return result

    def test_swagger(self):
        file_name = os.path.join('Data', 'Swagger.yaml')
        yaml_data = None
        with open(file_name, "rt") as stream:
            try:
                yaml_data = yaml.safe_load(stream)
            except yaml.YAMLError as ex:
                print(ex)
        if yaml_data is not None:
            paths = yaml_data['paths']
            path_key = list(paths.keys())[0]
            # Todo: handle also other paths
            path_all_data = paths[path_key]
            path_data = path_all_data['get']
            # Todo: handle also other requests (not only get)
            produces = path_data['produces'][0]
            # Todo: handle also xml
            parameters = path_data['parameters']
            params_url = str(path_key)      # clone
            for parameter in parameters:
                param_name = parameter['name']
                value = str(self.book_result[param_name])
                params_url = params_url.replace('{' + param_name + '}', value, 1)
            # Todo: handle also spaces in book name
            host, schema = yaml_data['host'], yaml_data['schemes'][0]
            # Todo: handle also https
            url = f'{schema}://{host}:5000{params_url}'
            print(f'url:{url}')
            header = {'accept': produces}
            result = requests.get(url, headers=header)
            json_dict_res = json.loads(result.text)
            if json_dict_res != self.book_result:
                print(f'test response error. got {result}')
            else:
                print('test worked ok')



