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
        with open(file_name, "rt") as stream:
            try:
                yaml_data = yaml.safe_load(stream)
                host, schema = yaml_data['host'], yaml_data['schemes'][0]
                paths = yaml_data['paths']
                path_key = list(paths.keys())[0]
                path_all_data = paths[path_key]
                path_data = path_all_data['get']
                produces = path_data['produces'][0]
                parameters = path_data['parameters']
                params_url = str(path_key)
                params_url = params_url.replace('{', '')
                params_url = params_url.replace('}', '')
                for parameter in parameters:
                    param_name = parameter['name']
                    value = self.book_result[param_name]
                    true_value = str(value)
                    params_url = params_url.replace(param_name, true_value, 1)
                url = f'{schema}://{host}:5000{params_url}'
                print(f'url:{url}')
                header = {'accept': produces}
                result = requests.get(url, headers=header)
                json_dict_res = json.loads(result.text)
                if json_dict_res != self.book_result:
                    print(f'response error. got {result}')
                else:
                    print('worked ok')

            except yaml.YAMLError as ex:
                print(ex)



