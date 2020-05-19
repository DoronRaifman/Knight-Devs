"""
Demonstrating:
    class property              ClassMember (one for all instances) (vs instance member)
    class instance property     ClassInstanceMember (one for each instance)
"""


class _ClassMember(type):
    _fs: int = int(100e6 // 2)

    @property
    def fs(cls):
        return cls._fs

    @fs.setter
    def fs(cls, value: int):
        cls._fs = value


class ClassMember(object, metaclass=_ClassMember):
    pass


class ClassInstanceMember:
    def __init__(self):
        self._fs: int = int(100e6 // 2)

    @property
    def fs(self):
        return self._fs

    @fs.setter
    def fs(self, value: int):
        self._fs = value


if __name__ == '__main__':
    print("ClassMember properties")
    print(f"fs={ClassMember.fs}")
    ClassMember.fs = 200
    print(f"fs={ClassMember.fs}")

    print("ClassInstance properties")
    class_instance_member = ClassInstanceMember()
    print(f"fs={class_instance_member.fs}")
    class_instance_member.fs = 200
    print(f"fs={class_instance_member.fs}")


