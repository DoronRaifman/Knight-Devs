
from enum import Enum


class MyBaseEnum(Enum):
    @classmethod
    def get_by_value(cls, value):
        members = list(cls.__members__.values())
        values = {enum_val.value: enum_val for enum_val in members}
        if value in values:
            res = values[value]
        else:
            dict_values = list(values.keys())
            res = values[dict_values[0]]
            print(f"@@@ {cls.__name__}: value {value} is not valid, set to '{res.name}'={res.value}")
        return res


class MyEnum(MyBaseEnum):
    one = 1
    two = 2


if __name__ == '__main__':
    val = 3
    res = MyEnum.get_by_value(val)
    print(res)

    val = 2
    res = MyEnum.get_by_value(val)
    print(res)

