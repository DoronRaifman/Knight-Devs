

class BitFields:
    def __init__(self, value=0):
        self.value = value

    def set_field_val(self, value:int, start_bit:int, bit_count:int):
        mask = (1 << bit_count) - 1
        val = value & mask  # make sure fits field
        val = val << start_bit
        self.value |= val

    def get_field_val(self, start_bit:int, bit_count:int):
        val = self.value >> start_bit
        mask = (1 << bit_count) - 1
        return val & mask

    @staticmethod
    def get_field_val_from_val(value:int, start_bit:int, bit_count:int):
        val = value >> start_bit
        mask = (1 << bit_count) - 1
        return val & mask


if __name__ == '__main__':
    print(BitFields.get_field_val_from_val(0x004, 2, 1))
    print(BitFields.get_field_val_from_val(0x003, 0, 2))
    print(BitFields.get_field_val_from_val(0x00c, 2, 2))
    print(BitFields.get_field_val_from_val(0x018, 3, 2))

    bits_val = BitFields()
    bits_val.set_field_val(7, 3, 3)
    print(bits_val.value)
    print(bits_val.get_field_val(3, 3))

    bits_val = BitFields()
    bits_val.set_field_val(1, 0, 1)
    bits_val.set_field_val(1, 1, 1)
    print(bits_val.value)
