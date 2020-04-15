
from Learn.PurchaseList.Core.DBInstance import *
from Learn.PurchaseList.Core.PurchaseList import PurchaseList


def use_purchse_list():
    purchase_list = PurchaseList()
    purchase_list.connect()

    # purchase_list.fill_initial_data()
    purchase_list.print_db()

    items = purchase_list.find_items_by_name("חלב")
    for item in items:
        print(str(item))
    # root_items = purchase_list.get_root_items()
    # del_item = root_items[0]
    # purchase_list.delete_item_with_siblings(del_item['id'])

    purchase_list.disconnect()


if __name__ == '__main__':
    use_purchse_list()




