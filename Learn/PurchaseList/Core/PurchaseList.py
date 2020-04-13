
from Learn.PurchaseList.Core.DBInstance import *


class PurchaseList:
    def __init__(self):
        self.table_name = 'items'

    def connect(self):
        DBInstance.connect()

    def disconnect(self):
        DBInstance.disconnect()

    def get_root_items(self):
        records = DBInstance.find(self.table_name, "papa_id = 0")
        return records

    def get_item_siblings(self, papa_id: int):
        records = DBInstance.find(self.table_name, f"papa_id = {papa_id}")
        return records

    def find_item_by_id(self, item_id):
        records = DBInstance.find(self.table_name, f"item_id = {item_id}")
        return records[0]

    def find_items_by_name(self, item_name):
        records = DBInstance.find(self.table_name, f"item_name = {item_name}")
        return records

    def update_item(self, item_id:int, item_fields: dict):
        res = DBInstance.update(self.table_name, item_fields, f"id = {item_id}")
        return res

    def print_db(self):
        list_records = self.get_root_items()
        for list_record in list_records:
            papa_id = list_record['id']
            departments = self.get_item_siblings(papa_id)
            dep_names = ""
            for department in departments:
                dep_names += f"{department['item_name']}, "
            print(f"List: {list_record['item_name']}: departments: {dep_names}")

    def delete_item_with_siblings(self, item_id: int):
        pass
        # ToDo: Adi implement method



