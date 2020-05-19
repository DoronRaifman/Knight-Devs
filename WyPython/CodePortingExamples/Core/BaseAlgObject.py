"""
Author: Doron Raifman, draifman@gmail.com, Knight-Devs at https://knight-devs.com/
"""

from struct import *
import WyPython.CodePortingExamples.Core.LogHelper


class AlgException(Exception):
    def __init__(self, line):
        BaseAlgObject.logger.error(f"Alg exception {line}")
        super().__init__(line)


class BaseAlgObject:
    logger = WyPython.CodePortingExamples.Core.LogHelper.configure_log(name='default', logger_name='default')

    def __init__(self):
        self.full_data: dict = {}

    def do_work(self, full_data: dict):
        self.full_data = full_data
        return full_data

    @staticmethod
    def _unpack_fields(blob_fields_description: dict, blob_data: bytearray, unpacked_dict: dict,
                       start_offset: int = 0, is_force_indian_str=None):
        """
        unpack fields from blob_data using fields_description dict into unpacked_dict
        @param blob_fields_description:
        @param blob_data:
        @param start_offset:
        @return: None
        """
        for field_name, field_info in blob_fields_description.items():
            unpack_format, offset, var_size = field_info
            offset_actual = start_offset + offset
            try:
                if is_force_indian_str is not None:
                    unpack_format = unpack_format.replace(unpack_format[0:1], is_force_indian_str)
                res = unpack(unpack_format, blob_data[offset_actual:offset_actual+var_size])
            except Exception as ex:
                raise AlgException(
                    f"@@@ Exception unpack in field:{field_name}, format: {unpack_format}, ex={str(ex)}")
            field_value = res[0]
            unpacked_dict[field_name] = field_value

    @staticmethod
    def pack_fields(blob_fields_description: dict, blob_data: bytearray, fields_dict: dict, start_offset: int = 0):
        """
        pack fields from fields_dict into blob_data
        note: blob_data should have enough size to accommodate output
        @param blob_fields_description: fields description dict
        @param blob_data: bytearray to store packed Data
        @param fields_dict: fields Data in dict format
        @param start_offset: start offset of writes into blob
        @return:
        """
        for field_name, field_info in blob_fields_description.items():
            pack_format, offset, var_size = field_info
            field_val = fields_dict[field_name]
            try:
                pack_into(pack_format, blob_data, start_offset + offset, field_val)
            except Exception as ex:
                raise AlgException(f"@@@ Exception pack in field:{field_name}, val:{field_val}, format: {pack_format},"
                                   f"ex={str(ex)}")
