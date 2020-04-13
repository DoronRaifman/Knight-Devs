
from Learn.PurchaseList.Core.DBInstance import *
from Learn.PurchaseList.Core.PurchaseList import PurchaseList


def print_db():
    table_name = 'items'
    list_records = DBInstance.find(table_name, "papa_id = 0")
    for list_record in list_records:
        papa_id = list_record['id']
        departments = DBInstance.find(table_name, f"papa_id = {papa_id}")
        dep_names = ""
        for department in departments:
            dep_names += f"{department['item_name']}, "
        print(f"List: {list_record['item_name']}: departments: {dep_names}")


def fill_initial_data():
    table_name = 'items'
    list_names = ["חצי חינם", "טירה בשר", "טירה מכולת", ]
    list_departments = [
        ["בכניסה", "מקרר", "בשר", ],
        ["עמדה ראשית", "מקרר", "המבורגרים", ],
        ["שתיה", "ירקות", "מוצרי חלב", ],
    ]
    for list_index in range(len(list_names)):
        fields_data = {'item_name': list_names[list_index], 'papa_id': 0, }
        papa_id = DBInstance.insert(table_name, fields_data)
        departments = list_departments[list_index]
        for department_index in range(len(departments)):
            fields_data = {'item_name': departments[department_index], 'papa_id': papa_id, }
            item_id = DBInstance.insert(table_name, fields_data)
    print_db()


def test_some_stuff():
    table_name = 'items'
    fields_data1 = {
        'item_name': "חצי חינם",
        'papa_id': 0,
    }
    fields_data2 = {
        'item_name': "טירה בשר",
        'papa_id': 0,
    }
    fields_data3 = {
        'item_name': "טירה מכולת",
        'papa_id': 0,
    }
    # item_id = DBInstance.insert(table_name, fields_data1)
    # item_id = DBInstance.insert(table_name, fields_data2)
    # item_id = DBInstance.insert(table_name, fields_data3)
    # res = DBInstance.delete(table_name, f"id < 1000")
    # res = DBInstance.update(table_name, fields_data2, f"id = 5")
    records = DBInstance.find(table_name, "papa_id = 0")
    print_db()


def use_purchse_list():
    purchase_list = PurchaseList()
    # purchase_list.connect()
    purchase_list.print_db()
    root_items = purchase_list.get_root_items()
    del_item = root_items[0]
    purchase_list.delete_item_with_siblings(del_item['id'])
    # purchase_list.disconnect()


if __name__ == '__main__':
    DBInstance.connect()
    # test_some_stuff()
    # fill_initial_data()
    print_db()
    # use_purchse_list()
    DBInstance.disconnect()




