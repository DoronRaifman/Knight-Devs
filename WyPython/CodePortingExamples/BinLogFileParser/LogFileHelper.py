from struct import *

from WyPython.CodePortingExamples.Core.BaseAlgObject import BaseAlgObject, AlgException
from WyPython.CodePortingExamples.Core.EpocHelper import EpocDate
from WyPython.CodePortingExamples.BinLogFileParser.Enums import *

# *************************************************************************
# *                 New Log format with miliseconds data from version 1.71
# *				  TimeStamp - 4 bytes for time
# *                 MiliSeconds TimeStamp - 2 bytes for time
# *				  EID - Enumarated ID log type
# *				  DID - Enumarated Log Data ID log data type
# *				  LDA - Log Data - the actual data and the size is depend on the written size up to 50 bytes of data mainly for text
# *				  NUL - Null character
# *                 -----------------------------------------------------------------------------------------------------
# * Bytes position  ...| 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |10 |11 |12 |13 |14 |15 |16 |17 |18 |19 |20 |.......
# *				    ---|---------------|---|---|---|---|-------------------|---------------|---|---|---|---|-------------
# * Actual log Data ...| 02| 13| 0E| D0| 12|4F | 12|10 | A | R | I | E |NUL|02 |13 | 0D| 9 |34 |78 |02 | 0 |.......
# *				    ---|---------------|---|---|---|---|-------------------|---------------|---|---|---|---|-------------
# * Log Descripion  ...| Time stamp    |milSec |EID|DID| LDA               | Time stamp    |milSec |EID|DID|.......
# *				    ---|---------------|---|---|---|---|-------------------|---------------|---|---|---|---|-------------
# * Logic Logs Num. ---|-------- Log Number N -----------------------------|---- Log Number N+1 -----------|----------------------------
# *
# * *************************************************************************/


class LogFileHelper(BaseAlgObject):
    def __init__(self):
        super().__init__()

    @classmethod
    def parse_log_file(cls, payload: bytearray):
        payload_length = len(payload)
        records_list = []
        start_offset = 0
        while start_offset + UnpackHelper.header_len < payload_length:
            record = {}
            record, bytes_count = cls.extract_message(payload, record, start_offset)
            start_offset += bytes_count
            records_list.append(record)
        return records_list

    @classmethod
    def extract_message(cls, payload: bytearray, record: dict, start_offset: int):
        cls._unpack_fields(UnpackHelper.fields_description, payload, record, start_offset)
        record['time_stamp'] = EpocDate.epoc_datetime_to_datetime(record['time_stamp'])
        record_data_type = cls.get_message_types(record)
        data_bytes_count, format_str = \
            UnpackHelper.unpack_format[record_data_type] if record_data_type in UnpackHelper.unpack_format else (0, "")
        offset_actual = start_offset + UnpackHelper.header_len
        if len(format_str) != 0 and data_bytes_count > 0:
            payload_chunk = payload[offset_actual:offset_actual + data_bytes_count]
            try:
                if record_data_type == ELogRecordDataType.LogDataTypeString:
                    res = payload_chunk.split(b'\x00')
                    str_len = len(res[0])
                    res = unpack(f"{str_len}s", res[0])
                    data_bytes_count = str_len + 1
                else:
                    res = unpack(format_str, payload_chunk)
                res = res[0]
            except Exception as ex:
                raise AlgException(
                    f"@@@ Exception unpack in data_type:{record_data_type}, format: {format_str}, ex={str(ex)}")
        else:
            res = "No result"
        record['value'] = res
        data_bytes_count += UnpackHelper.header_len
        return record, data_bytes_count

    @classmethod
    def get_message_types(cls, record: dict):
        record_type = record['record_type']
        record['record_type'] = record_type =\
            ELogRecordType(record_type) if ELogRecordType.has_value(record_type) else ELogRecordType.Illegal
        record_data_type = record['record_data_type']
        record_data_type = ELogRecordDataType(record_data_type) if ELogRecordDataType.has_value(record_data_type) \
            else ELogRecordDataType.NoValue
        record['record_data_type'] = record_data_type
        record_sub_type = record['record_sub_type']
        if record_type == ELogRecordType.StatusRecord:
            record_sub_type = StatusMessage.values[record_sub_type] if record_sub_type < len(StatusMessage.values)\
                else "Undefined status type"
        elif record_type == ELogRecordType.EventRecord:
            record_sub_type = EventMessage.values[record_sub_type] if record_sub_type < len(EventMessage.values)\
                else "Undefined event type"
        else:
            record_type = ELogRecordType.Illegal
        record['record_sub_type'] = record_sub_type
        return record_data_type

