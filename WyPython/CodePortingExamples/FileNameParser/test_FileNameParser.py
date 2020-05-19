"""
Author: Doron Raifman, draifman@gmail.com, Knight-Devs at https://knight-devs.com/
"""


import time
from unittest import TestCase

from WyPython.CodePortingExamples.FileNameParser.Enums import *
from WyPython.CodePortingExamples.FileNameParser.FileNameParser import FileNameParser


class TestSQSInstance(TestCase):
    file_names = [
        # logs
        {'sFilename': 'auto_354678050252464_17_11_2015_15_24_00.log'},
        {'sFilename': 'autoR_354678050252464_17_11_2015_15_24_00.log'},
        {'sFilename': 'Test_354678051333768_22_02_2016_15_45_00.log'},
        {'sFilename': 'TestR_354678051333768_22_02_2016_15_45_00.log'},
        # wav
        {'sFilename': 'Test_354678051333768_22_02_2016_15_45_00_2_4_0_0_1962380_0_24_sig.wav'},
        {'sFilename': 'TestR_354678051333768_22_02_2016_15_45_00_2_4_5_0_0_1962380_0_24_sig.wav'},
        {'sFilename': 'auto_354678051333768_22_02_2016_15_45_00_2_4_0_0_1962380_0_24_sig.wav'},
        {'sFilename': 'autoR_354678051333768_22_02_2016_15_45_00_2_4_5_0_0_1962380_0_24_sig.wav'},
        # # pressure
        {'sFilename': 'Trendp_354678051272263_22_02_2016_13_18_35_000.bin'},
        {'sFilename': 'Transient_354678051272263_22_02_2016_13_18_35_000.bin'},
        # # alarm
        {'sFilename': 'Alarm_359658044010082_20_12_2015_23_34_30_000_9.txt'},
        # # config
        {'sFilename': 'config_359658044010082.bindat'},
        # # registration
        {'sFilename': 'reg_359658044010082.bindat'},
        # # firmware
        {'sFilename': 'firmware_2_89.bin'},
    ]
    split_exp_res = [
        # logs
        {'sFilename': 'auto_354678050252464_17_11_2015_15_24_00.log', 'FileType': EG4DeviceGeneralFileType.LogFile,
         'G4DeviceFileType': EG4DeviceFileType.LogFileGPSDevice, 'had_errors': False, 'DeviceID': '354678050252464',
         'SampleTime': '2015, 11, 17, 15, 24', 'sGPS': 'N/A', 'sGainUsed': 'N/A', 'nSyncDuration': 0,
         'NC1': 0, 'NC2': 0,
        },
        {
            'sFilename': 'auto_354678050252464_17_11_2015_15_24_00.log',
            'FileType': EG4DeviceGeneralFileType.LogFile, 'G4DeviceFileType': EG4DeviceFileType.LogFileGPSDevice,
            'DeviceID': '354678050252464', 'SampleTime': '17-11-2015_15:24:00', 'sGPS': 'N/A', 'sGainUsed': 'N/A',
            'nSyncDuration': 0,
        },
        {
            'sFilename': 'autoR_354678050252464_17_11_2015_15_24_00.log',
            'FileType': EG4DeviceGeneralFileType.LogFile, 'G4DeviceFileType': EG4DeviceFileType.LogFileRadioDevice,
            'DeviceID': '354678050252464', 'SampleTime': '17-11-2015_15:24:00', 'sGPS': 'N/A', 'sGainUsed': 'N/A',
            'nSyncDuration': 0,
        },
        {
            'sFilename': 'Test_354678051333768_22_02_2016_15_45_00.log',
            'FileType': EG4DeviceGeneralFileType.LogFile, 'G4DeviceFileType': EG4DeviceFileType.LogFileBitGPSDevice,
            'DeviceID': '354678051333768', 'SampleTime': '22-02-2016_15:45:00', 'sGPS': 'N/A', 'sGainUsed': 'N/A',
            'nSyncDuration': 0,
        },
        {
            'sFilename': 'TestR_354678051333768_22_02_2016_15_45_00.log',
            'FileType': EG4DeviceGeneralFileType.LogFile, 'G4DeviceFileType': EG4DeviceFileType.LogFileBitRadioDevice,
            'DeviceID': '354678051333768', 'SampleTime': '22-02-2016_15:45:00', 'sGPS': 'N/A', 'sGainUsed': 'N/A',
            'nSyncDuration': 0,
        },
        # wav
        {
            'sFilename': 'Test_354678051333768_22_02_2016_15_45_00_2_4_0_0_1962380_0_24_sig.wav',
            'FileType': EG4DeviceGeneralFileType.WavFile, 'G4DeviceFileType': EG4DeviceFileType.AudioFileBitGPSDevice,
            'DeviceID': '354678051333768', 'SampleTime': '22-02-2016_15:45:00',
            'sGPS': 'PPS', 'sGainUsed': '4', 'nSyncDuration': 0,

        },
        {
            'sFilename': 'TestR_354678051333768_22_02_2016_15_45_00_2_4_5_0_0_1962380_0_24_sig.wav',
            'FileType': EG4DeviceGeneralFileType.WavFile, 'G4DeviceFileType': EG4DeviceFileType.AudioFileBitRadioDevice,
            'DeviceID': '354678051333768', 'SampleTime': '22-02-2016_15:45:00',
            'sGPS': 'PPS', 'sGainUsed': '4', 'nSyncDuration': 5,
        },
        {
            'sFilename': 'auto_354678051333768_22_02_2016_15_45_00_2_4_0_0_1962380_0_24_sig.wav',
            'FileType': EG4DeviceGeneralFileType.WavFile, 'G4DeviceFileType': EG4DeviceFileType.AudioFileGPSDevice,
            'DeviceID': '354678051333768', 'SampleTime': '22-02-2016_15:45:00',
            'sGPS': 'PPS', 'sGainUsed': '4', 'nSyncDuration': 0,
        },
        {
            'sFilename': 'autoR_354678051333768_22_02_2016_15_45_00_2_4_5_0_0_1962380_0_24_sig.wav',
            'FileType': EG4DeviceGeneralFileType.WavFile, 'G4DeviceFileType': EG4DeviceFileType.AudioFileRadioDevice,
            'DeviceID': '354678051333768', 'SampleTime': '22-02-2016_15:45:00',
            'sGPS': 'PPS', 'sGainUsed': '4', 'nSyncDuration': 5,
        },
        # pressure
        {
            'sFilename': 'Trendp_354678051272263_22_02_2016_13_18_35_000.bin',
            'FileType': EG4DeviceGeneralFileType.PressureFile,
            'G4DeviceFileType': EG4DeviceFileType.PressureFileTrend,
            'DeviceID': '354678051272263', 'SampleTime': '22-02-2016_13:18:35',
            'sGPS': 'N/A', 'sGainUsed': 'N/A', 'nSyncDuration': 0,
        },
        {
            'sFilename': 'Transient_354678051272263_22_02_2016_13_18_35_000.bin',
            'FileType': EG4DeviceGeneralFileType.PressureFile,
            'G4DeviceFileType': EG4DeviceFileType.PressureFileTransient,
            'DeviceID': '354678051272263', 'SampleTime': '22-02-2016_13:18:35',
            'sGPS': 'N/A', 'sGainUsed': 'N/A', 'nSyncDuration': 0,
        },
        # Alarm
        {
            'sFilename': 'Alarm_359658044010082_20_12_2015_23_34_30_000_9.txt',
            'FileType': EG4DeviceGeneralFileType.AlarmFile,
            'G4DeviceFileType': EG4DeviceFileType.AlarmFile,
            'DeviceID': '359658044010082', 'SampleTime': '20-12-2015_23:34:30',
            'sGPS': 'N/A', 'sGainUsed': 'N/A', 'nSyncDuration': 0,
        },
        # config
        {
            'sFilename': 'config_359658044010082.bindat',
            'FileType': EG4DeviceGeneralFileType.ConfigFile,
            'G4DeviceFileType': EG4DeviceFileType.ConfigFile,
            'DeviceID': '359658044010082',
        },
        # registration
        {
            'sFilename': 'reg_359658044010082.bindat',
            'FileType': EG4DeviceGeneralFileType.RegistrationFile,
            'G4DeviceFileType': EG4DeviceFileType.RegistrationFile,
            'DeviceID': '359658044010082',
        },
        # firmware
        {
            'sFilename': 'firmware_2_89.bin',
            'FileType': EG4DeviceGeneralFileType.FirmwareFile,
            'G4DeviceFileType': EG4DeviceFileType.FirmwareFile,
        },
    ]

    def test_split_file_name(self):
        for ndx, file_data in enumerate(self.file_names):
            file_data_res = FileNameParser.split_file_name(file_data)
            # print(f"got: {file_data}\n  res: {file_data_res}")
            self.assertEqual(file_data_res, self.split_exp_res[ndx])
