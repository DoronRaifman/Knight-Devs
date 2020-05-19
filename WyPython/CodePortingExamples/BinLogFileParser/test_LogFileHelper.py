"""
Author: Doron Raifman, draifman@gmail.com, Knight-Devs at https://knight-devs.com/
"""

import os
from datetime import datetime
from unittest import TestCase

from WyPython.CodePortingExamples.BinLogFileParser.LogFileHelper import \
    LogFileHelper, ELogRecordDataType, ELogRecordType


class TestLogHelper(TestCase):
    def test_parse_log_file(self):
        file_list = [
            "auto_354678050259576_28_01_2020_22_30_00.637158501210537396.log",
            "Test_354678050247357_29_10_2019_10_20_24.637079720357386143.log",
        ]
        expected_last_records = [
            {'time_stamp': datetime(2020, 1, 28, 23, 12, 59), 'milli_sec': 902,
             'record_type': ELogRecordType.StatusRecord, 'record_sub_type': 'status_last_reset_id_number',
             'record_data_type': ELogRecordDataType.LogDataTypeUInt, 'value': 0},
            {
             'time_stamp': datetime(2019, 10, 29, 18, 52, 52), 'milli_sec': 19,
             'record_type': ELogRecordType.StatusRecord, 'record_sub_type': 'status_last_reset_id_number',
             'record_data_type': ELogRecordDataType.LogDataTypeUInt, 'value': 0
            },
        ]
        for i, file in enumerate(file_list):
            file_name = os.path.abspath(os.path.join("Data", file))
            with open(file_name, "rb") as fdes:
                file_data = fdes.read()
                record_list = LogFileHelper.parse_log_file(bytearray(file_data))
                res = record_list[-1]
                self.assertEqual(res, expected_last_records[i])


