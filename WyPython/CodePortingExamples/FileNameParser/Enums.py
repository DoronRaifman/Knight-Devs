"""
Author: Doron Raifman, draifman@gmail.com, Knight-Devs at https://knight-devs.com/
"""

from enum import Enum


class EG4DeviceFileType(Enum):
    Unknown = 0
    LogFileGPSDevice = 1
    LogFileRadioDevice = 2
    LogFileBitGPSDevice = 3
    LogFileBitRadioDevice = 4
    AudioFileGPSDevice = 5
    AudioFileRadioDevice = 6
    AudioFileBitGPSDevice = 7
    AudioFileBitRadioDevice = 8
    PressureFileTrend = 9
    PressureFileTransient = 10
    AlarmFile = 11
    ConfigFile = 12
    RegistrationFile = 13
    FirmwareFile = 14


class EG4DeviceGeneralFileType(Enum):
    Unknown = 0
    LogFile = 1
    WavFile = 2
    AlarmFile = 3
    PressureFile = 4
    ConfigFile = 5
    FirmwareFile = 6
    RegistrationFile = 7


class EMessageType(Enum):
    ACK = 0x04  # @@guy: for debug
    NAK = 0x05  # @@guy: for debug

    # General
    KeepAlive = 0x01
    SetClock = 0x02
    GetClock = 0x03
    Reset = 0x07
    FirmwareUpdate = 0x08
    WaitForData = 0x60

    # Device activation
    SetDeviceID = 0x0B
    SetServer = 0x0E  # @@guy: is from site as well
    SetBank = 0x11
    SetActivation = 0x14  # @@guy: is from site as well

    # Information reports
    ReportDeviceInfo = 0x36
    ReportInstallationInfo = 0x37
    ReportDeviceStatus = 0x38

    # Audio Configuration
    GetAudioConfiguration = 0x39
    SetAudioConfiguration = 0x40
    ReportAudioConfiguration = 0x41

    # Scheduler Configuration
    GetSchedulerConfiguration = 0x42
    SetSchedulerConfiguration = 0x43
    ReportSchedulerConfiguration = 0x44

    # FM Receiver Configuration
    GetFmReceiverConfiguration = 0x45
    SetFmReceiverConfiguration = 0x46
    ReportFmReceiverConfiguration = 0x47

    # Correlation Configuration
    GetCorrelationConfiguration = 0x49
    SetCorrelationConfiguration = 0x50
    ReportCorrelationConfiguration = 0x51

    # Report Data
    ReportCorrelationData = 0x52
    ReportHighPriorityCorrelationData = 0x8052
    ReportNoiseIntensityData = 0x53
    ReportHighPriorityNoiseIntensityData = 0x8053
    ReportAudioData = 0x54
    ReportHighPriorityAudioData = 0x8054
    ReportFmReceiverScanning = 0x8000

    # High Priority Responses
    SelectFmChannel = 0x8001
    BitResponse = 0x8002
    HighPriorityBitRequest = 0x8003

    # Calc messages
    CalcAudioData = 0x09001
    CalcCorrSample = 0x09002
    CalcCorrInstance = 0x09003
    CalcFindPairs = 0x09004

    # G4Devices messages
    G4DeviceReportBitAudioData = 0x7001
    G4DeviceReportBitLogData = 0x7002
    G4DeviceReportAudioData = 0x7003
    G4DeviceReportLogData = 0x7004
    G4DeviceReportPressureTrendData = 0x7005
    G4DeviceReportPressureTransientData = 0x7006


class EGPSType(Enum):
    GpsTypeNone = -1
    GpsTypeRtc = 0
    GpsTypeSpi = 1
    GpsTypePps = 2

    @classmethod
    def get_gps_short_name(cls, gps_type: int):
        short_names = {
            cls.GpsTypeNone.value: "N.A",
            cls.GpsTypeRtc.value: "RTC",
            cls.GpsTypeSpi.value: "SPI",
            cls.GpsTypePps.value: "PPS",
        }
        res = short_names[gps_type] if gps_type in short_names else "Undef"
        return res


class ParseHelper:
        g4device_file_type_dict = {
            ('llauto', 'log'): (EG4DeviceFileType.LogFileGPSDevice, EG4DeviceGeneralFileType.LogFile),
            ('llautor', 'log'): (EG4DeviceFileType.LogFileGPSDevice, EG4DeviceGeneralFileType.LogFile),
            ('auto', 'log'): (EG4DeviceFileType.LogFileGPSDevice, EG4DeviceGeneralFileType.LogFile),
            ('autor', 'log'): (EG4DeviceFileType.LogFileRadioDevice, EG4DeviceGeneralFileType.LogFile),
            ('test', 'log'): (EG4DeviceFileType.LogFileBitGPSDevice, EG4DeviceGeneralFileType.LogFile),
            ('testr', 'log'): (EG4DeviceFileType.LogFileBitRadioDevice, EG4DeviceGeneralFileType.LogFile),
            ('auto', 'wav'): (EG4DeviceFileType.AudioFileGPSDevice, EG4DeviceGeneralFileType.WavFile,),
            ('autor', 'wav'): (EG4DeviceFileType.AudioFileRadioDevice, EG4DeviceGeneralFileType.WavFile),
            ('test', 'wav'): (EG4DeviceFileType.AudioFileBitGPSDevice, EG4DeviceGeneralFileType.WavFile),
            ('testr', 'wav'): (EG4DeviceFileType.AudioFileBitRadioDevice, EG4DeviceGeneralFileType.WavFile),
            ('trendp', 'bin'): (EG4DeviceFileType.PressureFileTrend, EG4DeviceGeneralFileType.PressureFile),
            ('transient', 'bin'): (EG4DeviceFileType.PressureFileTransient, EG4DeviceGeneralFileType.PressureFile),
            ('alarm', 'txt'): (EG4DeviceFileType.AlarmFile, EG4DeviceGeneralFileType.AlarmFile),
            ('config', 'bindat'): (EG4DeviceFileType.ConfigFile, EG4DeviceGeneralFileType.ConfigFile),
            ('reg', 'bindat'): (EG4DeviceFileType.RegistrationFile, EG4DeviceGeneralFileType.RegistrationFile),
            ('firmware', 'bin'): (EG4DeviceFileType.FirmwareFile, EG4DeviceGeneralFileType.FirmwareFile),
        }
        message_type_dict = {
            ('auto', 'wav'): EMessageType.G4DeviceReportAudioData,
            ('auto', 'log'): EMessageType.G4DeviceReportLogData,
            ('test', 'wav'): EMessageType.G4DeviceReportBitAudioData,
            ('test', 'log'): EMessageType.G4DeviceReportBitLogData,
            ('trend', 'bin'): EMessageType.G4DeviceReportPressureTrendData,
            ('transient', 'bin'): EMessageType.G4DeviceReportPressureTransientData,
        }
