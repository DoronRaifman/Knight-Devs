
from Learn.ApplicationExamples.PurchaseList.Core.DBInstance import *


class PurchaseListException(Exception):
    pass


def db_call(function):
    def wrapper(self, *args, **kwargs):
        try:
            res = function(self, *args, **kwargs)
        except DBException as ex:
            line = f"DB call exception: {str(ex)}"
            print(line)
            raise PurchaseListException(line)
        return res
    return wrapper


class Item:
    def __init__(self, item_dict: dict):
        self._id: int = item_dict['id']
        self.item_name: str = item_dict['item_name']
        self.papa_id: int = item_dict['papa_id']
        self.order_id: int = item_dict['order_id']
        self.sons: list = []

    def __str__(self):
        return f"id:{self.id}, name:'{self.item_name}', papa:{self.papa_id}, order_id:{self.order_id}"

    def __repr__(self):
        # return f"{self.item_name}:{self.sons}"
        return f"{self.item_name}:{self.sons}" if len(self.sons) > 0 else self.item_name

    @property
    def id(self):
        return self._id

    # Note: we don't allow setter, only constructor modify value
    # if try to use setter will raise AttributeError exception
    @id.setter
    def id(self, value: int):
        raise PurchaseListException("Try to set id. It is forbidden")
        # self._id = value


class PurchaseList:
    def __init__(self):
        self.table_name = 'items'

    @db_call
    def connect(self):
        DBInstance.connect()

    @db_call
    def disconnect(self):
        DBInstance.disconnect()

    @db_call
    def add_item(self, item_name: str, papa_id: int = 0, order_id: int = 5):
        fields_data = {'item_name': item_name, 'papa_id': papa_id, 'order_id': order_id}
        item_id = DBInstance.insert(self.table_name, fields_data)
        return item_id

    @db_call
    def update_item(self, item_id:int, item_fields: dict):
        res = DBInstance.update(self.table_name, item_fields, where_clause=f"id = {item_id}")
        return res

    @db_call
    def delete_all_items(self):
        DBInstance.delete(self.table_name, where_clause='id > 0')

    @db_call
    def delete_item_with_all_siblings(self, item_id: int):
        pass
        # ToDo: Adi implement method

    @db_call
    def get_item_siblings(self, papa_id: int):
        records_dict = DBInstance.find(self.table_name, where_clause=f"papa_id = {papa_id}", order_by="order_id ASC")
        records = [Item(record) for record in records_dict]
        return list(records)

    def get_item_siblings_recursive(self, papa_id: int = 0):
        items_list = self.get_item_siblings(papa_id)
        for item in items_list:
            sons = self.get_item_siblings_recursive(item.id)
            # item.sons = sons if len(sons) > 0 else None
            item.sons = sons
        return items_list

    def get_root_items(self):
        return self.get_item_siblings(papa_id=0)

    def item_list_to_dict_list(self, items):
        items_dict_list = []
        for item in items:
            item_dict = item.__dict__
            item_dict['id'] = item_dict['_id']
            del item_dict['sons']
            del item_dict['_id']
            items_dict_list.append(item_dict)
        return items_dict_list

    @db_call
    def find_item_by_id(self, item_id: int):
        records = DBInstance.find(self.table_name, where_clause=f"id = {item_id}")
        return Item(records[0])

    @db_call
    def find_items_by_name(self, item_name: str):
        records_dict = DBInstance.find(self.table_name, where_clause=f"item_name = '{item_name}'")
        records = [Item(record) for record in records_dict]
        return records

    @db_call
    def find_items_by_name_start(self, item_name: str):
        records_dict = DBInstance.find(self.table_name, where_clause=f"item_name LIKE '{item_name}%'")
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




