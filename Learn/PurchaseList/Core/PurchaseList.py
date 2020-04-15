
from Learn.PurchaseList.Core.DBInstance import *


class Item:
    def __init__(self, item_dict: dict):
        self.id = item_dict['id']
        self.item_name = item_dict['item_name']
        self.papa_id = item_dict['papa_id']
        self.order_id = item_dict['order_id']
        self.sons = None

    def __str__(self):
        return f"id:{self.id}, name:'{self.item_name}', papa:{self.papa_id}, order:{self.order_id}"

    def __repr__(self):
        return f"{self.item_name}:{self.sons}" if self.sons is not None else f"{self.item_name}"


class PurchaseList:
    def __init__(self):
        self.table_name = 'items'

    def connect(self):
        DBInstance.connect()

    def disconnect(self):
        DBInstance.disconnect()

    def add_item(self, item_name: str, papa_id: int = 0, order_id: int = 5):
        fields_data = {'item_name': item_name, 'papa_id': papa_id, 'order_id': order_id}
        item_id = DBInstance.insert(self.table_name, fields_data)
        return item_id

    def update_item(self, item_id:int, item_fields: dict):
        res = DBInstance.update(self.table_name, item_fields, f"id = {item_id}")
        return res

    def delete_all_items(self):
        DBInstance.delete(self.table_name, 'id > 0')

    def delete_item_with_siblings(self, item_id: int):
        pass
        # ToDo: Adi implement method

    def get_root_items(self):
        return self.get_item_siblings(papa_id=0)

    def get_item_siblings(self, papa_id: int):
        records_dict = DBInstance.find(self.table_name, where_clause=f"papa_id = {papa_id}")
        records = [Item(record) for record in records_dict]
        return list(records)

    def find_item_by_id(self, item_id):
        records = DBInstance.find(self.table_name, f"item_id = {item_id}")
        return Item(records[0])

    def find_items_by_name(self, item_name):
        records_dict = DBInstance.find(self.table_name, f"item_name = '{item_name}'")
        records = [Item(record) for record in records_dict]
        return records

    def fill_initial_data(self):
        table_name = 'items'
        list_names = ["חצי חינם", "טירה בשר", "סופר סופר", ]
        list_departments = [
            [("בכניסה", ["נייר טואלט", "אקונומיקה", "אבקת כביסה", ]),
             ("שתיה", ["7up", "מים", ]),
             ("מקרר", ["חלב", "גבינה צהובה", "קוטג", ]),
             ("בשר", ["כרעיים", "כנפיים", "שניצלים", ]),
             ],
            [("עמדה ראשית", ["אנטריקוט", "צלי כתף", ]),
             ("מקרר", ["שרימפס", "בשר טחון קפוא"]),
             ("המבורגרים", ["המבורגרים",]),
             ],
            [("שתיה", ["7up", "מים", ]),
             ("ירקות", ["עגבניות", "מלפפונים", ]),
             ("מוצרי חלב", ["חלב", "גבינה צהובה", "קוטג", ]),
             ],
        ]
        self.delete_all_items()
        for list_index in range(len(list_names)):
            papa_id = self.add_item(list_names[list_index], papa_id=0)
            departments = list_departments[list_index]
            for department_index in range(len(departments)):
                dep_name, items = departments[department_index]
                dep_id = self.add_item(dep_name, papa_id=papa_id)
                for item_name in items:
                    item_id = self.add_item(item_name, papa_id=dep_id)

    def get_recursive_items(self, papa_id: int = 0):
        items_list = self.get_item_siblings(papa_id)
        for item in items_list:
            sons = self.get_recursive_items(item.id)
            item.sons = sons if len(sons) > 0 else None
        return items_list

    def print_db(self):
        list_records = self.get_root_items()
        for list_record in list_records:
            sons = self.get_recursive_items(papa_id=list_record.id)
            print(f"{list_record.item_name}: {sons}")




