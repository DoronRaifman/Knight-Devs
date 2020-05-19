"""
file name parser

Original CS code (included): 600 very little of them is enums
Python: 85 lines of code + 150 enums and data = 235 lines of code

very simple and clear 85 lines of code
most of the code is enums and data (no need to unit test)
unit test and test data included
easy to maintain

I started the job by using Python code that was translated 1 to 1 from CS and was written in python CS style
I developed unit test that test all the cases.
I used the result data of all the test cases and saved the result to the expected result in the unit test
Then I rewrote the code in python style and was relaxed that when I will pass the unit test I am done.

Author: Doron Raifman, draifman@gmail.com, Knight-Devs at https://knight-devs.com/
"""

import os
import datetime

from WyPython.CodePortingExamples.Core.BaseAlgObject import BaseAlgObject
from WyPython.CodePortingExamples.FileNameParser.Enums import *


class FileNameParser:
    @classmethod
    def split_file_name(cls, file_data: dict):
        file_data_res = file_data
        file_name = file_data_res["sFilename"]

        file_name_part, file_ext_part = os.path.splitext(file_name.lower())
        file_name_part_splited = file_name_part.split("_")
        file_name_part_g4device_file_type = file_name_part_splited[0]
        file_name_ext_part_type = file_ext_part[1:]
        for key, value in ParseHelper.g4device_file_type_dict.items():
            name_part, ext_part = key
            file_type, general_file_type = value
            if name_part == file_name_part_g4device_file_type and ext_part == file_name_ext_part_type:
                file_data_res["FileType"] = general_file_type
                file_data_res["G4DeviceFileType"] = file_type

        file_data_res['had_errors'] = False
        if "FileType" not in file_data_res:
            file_data_res["FileType"] = EG4DeviceGeneralFileType.Unknown
            file_data_res['had_errors'] = True

        if file_data_res["FileType"] != EG4DeviceGeneralFileType.FirmwareFile:
            file_data_res["DeviceID"] = file_name_part_splited[1]

        if len(file_name_part_splited) > 7:
            try:
                cls.parse_file_name(file_data_res, file_name_part_splited)
            except Exception as ex:
                BaseAlgObject.logger.error(f"Error deleting bad message {ex}")
                file_data_res['had_errors'] = True
        return file_data_res

    @classmethod
    def parse_file_name(cls, file_data: dict, file_name_part_splited: list):
        file_data["SampleTime"] = cls.get_sample_time(file_name_part_splited)
        file_data["sGPS"] = "N/A"
        file_data["sGainUsed"] = "N/A"
        file_data["nSyncDuration"] = 0
        file_data["NC1"] = 0
        file_data["NC2"] = 0
        if file_data["FileType"] == EG4DeviceGeneralFileType.WavFile:
            file_data["sGPS"] = EGPSType.get_gps_short_name(int(file_name_part_splited[8]))
            file_data["sGainUsed"] = file_name_part_splited[9]
        if (file_data["G4DeviceFileType"] == EG4DeviceFileType.AudioFileRadioDevice or
                file_data["G4DeviceFileType"] == EG4DeviceFileType.AudioFileBitRadioDevice):
            file_data["nSyncDuration"] = int(file_name_part_splited[10])
            file_data["NC1"] = file_name_part_splited[14]
            file_data["NC2"] = file_name_part_splited[15]
        elif file_data["FileType"] == EG4DeviceGeneralFileType.WavFile:
            file_data["NC1"] = file_name_part_splited[13]
            file_data["NC2"] = file_name_part_splited[14]

        return True

    @classmethod
    def get_sample_time(cls, file_name_part_splited):
        sample_time_format = "{}-{}-{}_{}:{}:{}"
        sample_time_str = sample_time_format.format(
            file_name_part_splited[2], file_name_part_splited[3],
            file_name_part_splited[4], file_name_part_splited[5],
            file_name_part_splited[6], file_name_part_splited[7])
        if (sample_time_str.find('.') >= 0):
            sample_time_str = sample_time_str.split(".")[0]
        return datetime.datetime.strptime(sample_time_str, '%d-%m-%Y_%H:%M:%S')

    # todo: implement get_message_type based on G4DevicFileType switch
    @staticmethod
    def get_message_type(filename):
        message_type = None
        file_name_part, file_name_ext_part = os.path.splitext(filename.lower())
        file_name_ext_part_type = file_name_ext_part[1:]
        for key, msg_type in ParseHelper.message_type_dict.items():
            name_part, ext_part = key
            if name_part in file_name_part and ext_part == file_name_ext_part_type:
                message_type = msg_type
                break
        return message_type

