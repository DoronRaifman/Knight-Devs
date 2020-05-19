
from enum import Enum


class MyEnum(Enum):
    one = 1
    two = 2


class MyClass:
    def __init__(self):
        self.enum_var = MyEnum.one


if __name__ == '__main__':
    my_class = MyClass()

    my_enum_member = my_class.__dict__['enum_var']
    my_enum_type = type(my_enum_member)
    my_enum_use = my_enum_type.two


    i = 7



