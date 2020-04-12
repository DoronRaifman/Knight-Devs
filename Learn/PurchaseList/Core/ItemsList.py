

class ItemsList:
    next_item_id = 1

    def __init__(self, name: str):
        self.item_name: str = name
        self.papa: ItemsList = None
        self.item_id = ItemsList.next_item_id
        ItemsList.next_item_id += 1
        self.sons = {}

    def __str__(self):
        return f'Item: {self.item_name}, have {len(self.sons)} sons'

    def __repr__(self):
        return str(self)

    def add(self, item_name):
        item = ItemsList(item_name)
        item.papa = self
        self.sons[item.item_id] = item
        return item

    def delete(self, item_id):
        del self.sons[item_id]

    def get_items(self):
        return list(self.sons.values())

    def find_item_by_id(self, item_id: int):
        return self.sons[item_id]

    def find_item_by_name(self, name: str):
        return [item for item in self.sons if name == item.item_name][0]

    def find_item_list_by_partial_name(self, name: str):
        return [item for item in self.sons if name in item.item_name]


