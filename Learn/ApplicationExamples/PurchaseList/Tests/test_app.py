from Learn.ApplicationExamples.PurchaseList.Core.PurchaseList import PurchaseList


def replace_order_id():
    purchase_list = PurchaseList()
    purchase_list.connect()

    update_list = [("מ", 3), ("כ", 7)]
    for search_name, order_id in update_list:
        items = purchase_list.find_items_by_name_start(search_name)
        for item in items:
            update_fields = {"order_id": order_id}
            purchase_list.update_item(item.id, update_fields)

    purchase_list.print_db()
    # start with
    for search_name, order_id in update_list:
        print(f"start with '{search_name}'")
        items = purchase_list.find_items_by_name_start(search_name)
        for item in items:
            print(str(item))

    purchase_list.disconnect()


def search_by_name():
    purchase_list = PurchaseList()
    purchase_list.connect()

    search_name = "חלב"
    print(f"find by name '{search_name}'")
    items = purchase_list.find_items_by_name(search_name)
    for item in items:
        print(str(item))

    purchase_list.disconnect()


def delete_item_with_siblings():
    purchase_list = PurchaseList()
    purchase_list.connect()

    root_items = purchase_list.get_root_items()
    del_item = root_items[0]
    purchase_list.delete_item_with_all_siblings(del_item['id'])

    purchase_list.disconnect()


def print_db():
    purchase_list = PurchaseList()
    purchase_list.connect()

    # purchase_list.fill_initial_data()
    purchase_list.print_db()

    purchase_list.disconnect()


if __name__ == '__main__':
    print_db()
    search_by_name()




