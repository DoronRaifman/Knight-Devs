using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prod3Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    //using SplitAquariousFilename;

    /*************************************************************************
    *	Name 		:
    *	Description	: The log will be of data :
    *				  TimeStamp - 4 bytes for time
    *				  EID - Enumarated ID log type
    *				  DID - Enumarated Log Data ID log data type
    *				  LDA - Log Data - the actual data and the size is depend on the written size up to 50 bytes of data mainly for text
    *				  NUL - Null character
    *                 ----------------------------------------------------------------------------------
    * Bytes position  ...| 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |10 |11 |12 |13 |14 |15 |16 |.......
    *				  ---|---------------|---|---|-------------------|---------------|---|---|-------------
    * Actual log Data ...| 02| 13| 0E| D0| 12|10 | A | R | I | E |NUL|02 |13 | 0D| 9 |02 | 0 |.......
    *				  ---|---------------|---|---|-------------------|---------------|---|---|-------------
    * Log Descripion  ...| Time stamp    |EID|DID| LDA               | Time stamp    |EID|DID|.......
    *				  ---|---------------|---|---|-------------------|---------------|---|---|-------------
    * Logic Logs Num. ---|-------- Log Number N ---------------------|---- Log Number N+1 ---|-------------
    *
    * 
    *************************************************************************/

    public struct LogFileData
    {
        public int iGPSSyncType;  // eLOG_SET_GPS_SYNC_TO
        public float floatGPSPosLatitude;   // eLOG_GPS_POSITION_LATITUDE  -  3151.1180
        public string sloatGPSPosQuadns;     //eLOG_GPS_POSITION_QUADNS  -  N
        public float floatGPSPosLongitude;  //eLOG_GPS_POSITION_LONGITUDE  -  3513.5000
        public string sloatGPSPosQuadew;     //eLOG_GPS_POSITION_QUADEW  -  E
        public float floatGPSPosElevation;  //eLOG_GPS_POSITION_ELEVATION  -  0.0000
        public string sSimCcid; //eLOG_SIM_CCID
        public int iGainUsed;   //eLOG_GAIN_USED
        public string sHardwareVersion; //eLOG_HW_VERSION
        public string sFirmwareBuild;   //eLOG_FIRMWARE_BUILD
        public string sFirmwareVerison; //eLOG_FIRMWARE_VERSION
        public string sModemID;

        public string m_sUnitName;
        public string m_sTransmitTime;

        public string m_sCalculatingGainSTD;
        public string m_sModemOpenedOKAfterNumRetries;
        public string m_sModemClosedOKAfterNumRetries;
        public string m_sBatteryVoltageTooLowToOpenModem;
        public string m_sFTPConnectedOKAfterNumRetries;
        public string m_sFTPConnectedFailedAfterNumRetries;
        public string m_sPPSPulseReceivedWithNumSatelites;
        public string m_sModemCommandTimedoutOnCommand;
        public string m_sSyncronizedWithGPSSPITimeWithNumSatelites;
        public string m_sSettingDateFromGPS;
        public string m_sModemCommandFailed;
        public string m_sModemAtennaSignalStrength;
        public string m_sRadioLockChannelStatus;
        public string m_sRadioLockChannelFrequencyNumber;
        public string m_sRadioLockChannelSNR;
        public string m_sRadioSetTimeFromModem;
        public string m_sLogDateTime;
        public string m_sFirmwareBuildType;
        public string m_sFirmwareVersion;
        public string m_sHardwareVersion;
        public string m_sServerIP;
        public string m_sConfigFileVersion;
        public string m_sRecordingTime;
        public string m_sTransmitDay;
        public string m_sRecordingTime1;
        public string m_sRecordingTime2;
        public string m_sRecordingTime3;
        public string m_sSamplingRate;
        public string m_sGainDefine;
        public string m_sGainThreshold1;
        public string m_sGainThreshold2;
        public string m_sGainThreshold3;
        public string m_sGPSWakeupTimeBeforeRecording;
        public string m_sGainWakeupTimeBeforeRecording;
        public string m_sGainCalculationDuration;
        public string m_sFTPRetriesNumbers;
        public string m_sTransmitWindowStartTime;
        public string m_sTransmitWindowStartTimeModuloNumber;
        public string m_sResetIDNumber;
        public string m_sRunFullCycleAfterReset;
        public string m_sNoiseGainCalibrationTimeCycleDuration;
        public string m_sNoiseGainCalibrationTimeSampleDuration;
        public string m_sNoiseGainCalibrationWakeupTimeBeforeRecording;
        public string m_sGPSLatitude;
        public string m_sGPSQuadN_S;
        public string m_sGPSLongitude;
        public string m_sGPSQuadE_W;
        public string m_sGPSElevation;
        public string m_sGPSSyncType;
        public string m_sGPSStartRTCTime;
        public string m_sGPSEndRTCTime;
        public string m_sGPSSatelliteLockTime;
        public string m_sGainUsed;
        public string m_sGainAverageCalculated;
        public string m_sGainSTDCalculated;
        public string m_sStartRecordingTimeCorrectionVSGPS;
        public string m_sEndRecordingTime;
        public string m_sDeltaTimeBetweenGPSAndLocal;
        public string m_sLastSyncGPSPPSTime;
        public string m_sSIMCCID;
        public string m_sGPSUsage;
        public string m_sModemUsage;
        public string m_sBatteryVoltageBeforeRecording;
        public string m_sBatteryVoltageAfterRecording;
        public string m_sUnitTemperature;
        public string m_sFTPOpenOKFileName;
        public string m_sFTPOpenFailedFileName;
        public string m_sSendWaveHeaderFileStatus;
        public string m_sFTPFileSize;
        public string m_sSendFileNumberBuffers;
        public string m_sSendFileBufferZerolengthBufferIndex;
        public string m_sFTPFailedSendBufferIndex;
        public string m_sStartSendingFTPFile;
        public string m_sFininsingSendingFTPFile;
        public string m_sRegistrationFilename;
        public string m_sConfigurationFilename;
        public string m_sLastResetIDNumber;
        public string m_sResetIssueIDNumber;


        /// <summary>
        /// 
        /// </summary>
        private void ResetAllParamsToDefault()
        {
            m_sCalculatingGainSTD = "";
            m_sModemOpenedOKAfterNumRetries = "";
            m_sModemClosedOKAfterNumRetries = "";
            m_sBatteryVoltageTooLowToOpenModem = "";
            m_sFTPConnectedOKAfterNumRetries = "";
            m_sFTPConnectedFailedAfterNumRetries = "";
            m_sPPSPulseReceivedWithNumSatelites = "";
            m_sModemCommandTimedoutOnCommand = "";
            m_sSyncronizedWithGPSSPITimeWithNumSatelites = "";
            m_sSettingDateFromGPS = "";
            m_sModemCommandFailed = "";
            m_sModemAtennaSignalStrength = "";
            m_sRadioLockChannelStatus = "";
            m_sRadioLockChannelFrequencyNumber = "";
            m_sRadioLockChannelSNR = "";
            m_sRadioSetTimeFromModem = "";
            m_sLogDateTime = "";
            m_sFirmwareBuildType = "";
            m_sFirmwareVersion = "";
            m_sHardwareVersion = "";
            m_sServerIP = "";
            m_sConfigFileVersion = "";
            m_sRecordingTime = "";
            m_sTransmitDay = "";
            m_sRecordingTime1 = "";
            m_sRecordingTime2 = "";
            m_sRecordingTime3 = "";
            m_sSamplingRate = "";
            m_sGainDefine = "";
            m_sGainThreshold1 = "";
            m_sGainThreshold2 = "";
            m_sGainThreshold3 = "";
            m_sGPSWakeupTimeBeforeRecording = "";
            m_sGainWakeupTimeBeforeRecording = "";
            m_sGainCalculationDuration = "";
            m_sFTPRetriesNumbers = "";
            m_sTransmitWindowStartTime = "";
            m_sTransmitWindowStartTimeModuloNumber = "";
            m_sResetIDNumber = "";
            m_sRunFullCycleAfterReset = "";
            m_sNoiseGainCalibrationTimeCycleDuration = "";
            m_sNoiseGainCalibrationTimeSampleDuration = "";
            m_sNoiseGainCalibrationWakeupTimeBeforeRecording = "";
            m_sGPSLatitude = "";
            m_sGPSQuadN_S = "";
            m_sGPSLongitude = "";
            m_sGPSQuadE_W = "";
            m_sGPSElevation = "";
            m_sGPSSyncType = "";
            m_sGPSStartRTCTime = "";
            m_sGPSEndRTCTime = "";
            m_sGPSSatelliteLockTime = "";
            m_sGainUsed = "";
            m_sGainAverageCalculated = "";
            m_sGainSTDCalculated = "";
            m_sStartRecordingTimeCorrectionVSGPS = "";
            m_sEndRecordingTime = "";
            m_sDeltaTimeBetweenGPSAndLocal = "";
            m_sLastSyncGPSPPSTime = "";
            m_sSIMCCID = "";
            m_sGPSUsage = "";
            m_sModemUsage = "";
            m_sBatteryVoltageBeforeRecording = "";
            m_sBatteryVoltageAfterRecording = "";
            m_sUnitTemperature = "";
            m_sFTPOpenOKFileName = "";
            m_sFTPOpenFailedFileName = "";
            m_sSendWaveHeaderFileStatus = "";
            m_sFTPFileSize = "";
            m_sSendFileNumberBuffers = "";
            m_sSendFileBufferZerolengthBufferIndex = "";
            m_sFTPFailedSendBufferIndex = "";
            m_sFininsingSendingFTPFile = "";
            m_sRegistrationFilename = "";
            m_sConfigurationFilename = "";
            m_sLastResetIDNumber = "";
            m_sResetIssueIDNumber = "";
        }

        ////////////////////////////////////////////////////////////////

        //public LogFileData()
        //{
        //    iGPSSyncType = 0 ;  // eLOG_SET_GPS_SYNC_TO
        //    floatGPSPosLatitude = 0;   // eLOG_GPS_POSITION_LATITUDE  -  3151.1180
        //    sloatGPSPosQuadns = "-";     //eLOG_GPS_POSITION_QUADNS  -  N
        //    floatGPSPosLongitude = 0;  //eLOG_GPS_POSITION_LONGITUDE  -  3513.5000
        //    sloatGPSPosQuadew = "-";     //eLOG_GPS_POSITION_QUADEW  -  E
        //    floatGPSPosElevation = 0;  //eLOG_GPS_POSITION_ELEVATION  -  0.0000
        //    sSimCcid = "Unknown"; //eLOG_SIM_CCID
        //    iGainUsed = 0 ;   //eLOG_GAIN_USED
        //    sHardwareVersion = "Unknown"; //eLOG_HW_VERSION
        //    sFirmwareBuild = "Unknown";   //eLOG_FIRMWARE_BUILD
        //    sFirmwareVerison = AquariousFTP.FIRMWARE_VERSION_UNKNOWN; //eLOG_FIRMWARE_VERSION
        //    sModemID = "Unknown";
        //}

        public void Clear()
        {
            m_sUnitName = "";
            m_sTransmitTime = "";
            ResetAllParamsToDefault();
            iGPSSyncType = (int)SplitFileName.EGPSType.eGPS_NONE;  // eLOG_SET_GPS_SYNC_TO
            floatGPSPosLatitude = 0;   // eLOG_GPS_POSITION_LATITUDE  -  3151.1180
            sloatGPSPosQuadns = "-";     //eLOG_GPS_POSITION_QUADNS  -  N
            floatGPSPosLongitude = 0;  //eLOG_GPS_POSITION_LONGITUDE  -  3513.5000
            sloatGPSPosQuadew = "-";     //eLOG_GPS_POSITION_QUADEW  -  E
            floatGPSPosElevation = 0;  //eLOG_GPS_POSITION_ELEVATION  -  0.0000
            sSimCcid = "Unknown"; //eLOG_SIM_CCID
            iGainUsed = 0;   //eLOG_GAIN_USED
            sHardwareVersion = "Unknown"; //eLOG_HW_VERSION
            sFirmwareBuild = "Unknown";   //eLOG_FIRMWARE_BUILD
            sFirmwareVerison = SplitFileName.FIRMWARE_VERSION_UNKNOWN; //eLOG_FIRMWARE_VERSION
            sModemID = "Unknown";
        }

        public LogFileData(LogFileData logData)
        {
            this = logData;
            iGPSSyncType = logData.iGPSSyncType;  // eLOG_SET_GPS_SYNC_TO
            floatGPSPosLatitude = logData.floatGPSPosLatitude;   // eLOG_GPS_POSITION_LATITUDE  -  3151.1180
            sloatGPSPosQuadns = logData.sloatGPSPosQuadns;     //eLOG_GPS_POSITION_QUADNS  -  N
            floatGPSPosLongitude = logData.floatGPSPosLongitude;  //eLOG_GPS_POSITION_LONGITUDE  -  3513.5000
            sloatGPSPosQuadew = logData.sloatGPSPosQuadew;     //eLOG_GPS_POSITION_QUADEW  -  E
            floatGPSPosElevation = logData.floatGPSPosElevation;  //eLOG_GPS_POSITION_ELEVATION  -  0.0000
            sSimCcid = logData.sSimCcid; //eLOG_SIM_CCID
            iGainUsed = logData.iGainUsed;   //eLOG_GAIN_USED
            sHardwareVersion = logData.sHardwareVersion; //eLOG_HW_VERSION
            sFirmwareBuild = logData.sFirmwareBuild;   //eLOG_FIRMWARE_BUILD
            sFirmwareVerison = logData.sFirmwareVerison; //eLOG_FIRMWARE_VERSION
            sModemID = logData.sModemID;

            m_sUnitName = logData.m_sUnitName;
            m_sTransmitTime = logData.m_sTransmitTime;
            m_sCalculatingGainSTD = logData.m_sCalculatingGainSTD;
            m_sModemOpenedOKAfterNumRetries = logData.m_sModemOpenedOKAfterNumRetries;
            m_sModemClosedOKAfterNumRetries = logData.m_sModemClosedOKAfterNumRetries;
            m_sBatteryVoltageTooLowToOpenModem = logData.m_sBatteryVoltageTooLowToOpenModem;
            m_sFTPConnectedOKAfterNumRetries = logData.m_sFTPConnectedOKAfterNumRetries;
            m_sFTPConnectedFailedAfterNumRetries = logData.m_sFTPConnectedFailedAfterNumRetries;
            m_sPPSPulseReceivedWithNumSatelites = logData.m_sPPSPulseReceivedWithNumSatelites;
            m_sModemCommandTimedoutOnCommand = logData.m_sModemCommandTimedoutOnCommand;
            m_sSyncronizedWithGPSSPITimeWithNumSatelites = logData.m_sSyncronizedWithGPSSPITimeWithNumSatelites;
            m_sSettingDateFromGPS = logData.m_sSettingDateFromGPS;
            m_sModemCommandFailed = logData.m_sModemCommandFailed;
            m_sModemAtennaSignalStrength = logData.m_sModemAtennaSignalStrength;
            m_sRadioLockChannelStatus = logData.m_sRadioLockChannelStatus;
            m_sRadioLockChannelFrequencyNumber = logData.m_sRadioLockChannelFrequencyNumber;
            m_sRadioLockChannelSNR = logData.m_sRadioLockChannelSNR;
            m_sRadioSetTimeFromModem = logData.m_sRadioSetTimeFromModem;
            m_sLogDateTime = logData.m_sLogDateTime;
            m_sFirmwareBuildType = logData.m_sFirmwareBuildType;
            m_sFirmwareVersion = logData.m_sFirmwareVersion;
            m_sHardwareVersion = logData.m_sHardwareVersion;
            m_sServerIP = logData.m_sServerIP;
            m_sConfigFileVersion = logData.m_sConfigFileVersion;
            m_sRecordingTime = logData.m_sRecordingTime;
            m_sTransmitDay = logData.m_sTransmitDay;
            m_sRecordingTime1 = logData.m_sRecordingTime1;
            m_sRecordingTime2 = logData.m_sRecordingTime2;
            m_sRecordingTime3 = logData.m_sRecordingTime3;
            m_sSamplingRate = logData.m_sSamplingRate;
            m_sGainDefine = logData.m_sGainDefine;
            m_sGainThreshold1 = logData.m_sGainThreshold1;
            m_sGainThreshold2 = logData.m_sGainThreshold2;
            m_sGainThreshold3 = logData.m_sGainThreshold3;
            m_sGPSWakeupTimeBeforeRecording = logData.m_sGPSWakeupTimeBeforeRecording;
            m_sGainWakeupTimeBeforeRecording = logData.m_sGainWakeupTimeBeforeRecording;
            m_sGainCalculationDuration = logData.m_sGainCalculationDuration;
            m_sFTPRetriesNumbers = logData.m_sFTPRetriesNumbers;
            m_sTransmitWindowStartTime = logData.m_sTransmitWindowStartTime;
            m_sTransmitWindowStartTimeModuloNumber = logData.m_sTransmitWindowStartTimeModuloNumber;
            m_sResetIDNumber = logData.m_sResetIDNumber;
            m_sRunFullCycleAfterReset = logData.m_sRunFullCycleAfterReset;
            m_sNoiseGainCalibrationTimeCycleDuration = logData.m_sNoiseGainCalibrationTimeCycleDuration;
            m_sNoiseGainCalibrationTimeSampleDuration = logData.m_sNoiseGainCalibrationTimeSampleDuration;
            m_sNoiseGainCalibrationWakeupTimeBeforeRecording = logData.m_sNoiseGainCalibrationWakeupTimeBeforeRecording;
            m_sGPSLatitude = logData.m_sGPSLatitude;
            m_sGPSQuadN_S = logData.m_sGPSQuadN_S;
            m_sGPSLongitude = logData.m_sGPSLongitude;
            m_sGPSQuadE_W = logData.m_sGPSQuadE_W;
            m_sGPSElevation = logData.m_sGPSElevation;
            m_sGPSSyncType = logData.m_sGPSSyncType;
            m_sGPSStartRTCTime = logData.m_sGPSStartRTCTime;
            m_sGPSEndRTCTime = logData.m_sGPSEndRTCTime;
            m_sGPSSatelliteLockTime = logData.m_sGPSSatelliteLockTime;
            m_sGainUsed = logData.m_sGainUsed;
            m_sGainAverageCalculated = logData.m_sGainAverageCalculated;
            m_sGainSTDCalculated = logData.m_sGainSTDCalculated;
            m_sStartRecordingTimeCorrectionVSGPS = logData.m_sStartRecordingTimeCorrectionVSGPS;
            m_sEndRecordingTime = logData.m_sEndRecordingTime;
            m_sDeltaTimeBetweenGPSAndLocal = logData.m_sDeltaTimeBetweenGPSAndLocal;
            m_sLastSyncGPSPPSTime = logData.m_sLastSyncGPSPPSTime;
            m_sSIMCCID = logData.m_sSIMCCID;
            m_sGPSUsage = logData.m_sGPSUsage;
            m_sModemUsage = logData.m_sModemUsage;
            m_sBatteryVoltageBeforeRecording = logData.m_sBatteryVoltageBeforeRecording;
            m_sBatteryVoltageAfterRecording = logData.m_sBatteryVoltageAfterRecording;
            m_sUnitTemperature = logData.m_sUnitTemperature;
            m_sFTPOpenOKFileName = logData.m_sFTPOpenOKFileName;
            m_sFTPOpenFailedFileName = logData.m_sFTPOpenFailedFileName;
            m_sSendWaveHeaderFileStatus = logData.m_sSendWaveHeaderFileStatus;
            m_sFTPFileSize = logData.m_sFTPFileSize;
            m_sSendFileNumberBuffers = logData.m_sSendFileNumberBuffers;
            m_sSendFileBufferZerolengthBufferIndex = logData.m_sSendFileBufferZerolengthBufferIndex;
            m_sFTPFailedSendBufferIndex = logData.m_sFTPFailedSendBufferIndex;
            m_sFininsingSendingFTPFile = logData.m_sFininsingSendingFTPFile;
            m_sRegistrationFilename = logData.m_sRegistrationFilename;
            m_sConfigurationFilename = logData.m_sConfigurationFilename;
            m_sLastResetIDNumber = logData.m_sLastResetIDNumber;
            m_sResetIssueIDNumber = logData.m_sResetIssueIDNumber;
        }
    };

    public class LogFile
    {
        public List<ushort> listNTPData = new List<ushort>();
        public delegate void LogEventDelegate(string text);
        public event LogEventDelegate LogEvent;
        private const int MAX_LOG_DATA_SIZE = 50;   // eqch log cannot be more then 50 chars
        public List<string> lstLogData = new List<string>();
        public LogFileData logData = new LogFileData();

        private ELogFileType eLogFileType = ELogFileType.eLOG_FILE_TYPE_UNKNOWN;


        private enum ELogExtendedDataTypes
        {
            eLOG_DATA_TYPE_NONE = 0,
            eLOG_DATA_TYPE_BYTE,
            eLOG_DATA_TYPE_INT,
            eLOG_DATA_TYPE_LONG,
            eLOG_DATA_TYPE_UNSIGNED_SHORT,
            eLOG_DATA_TYPE_UNSIGNED_INT,
            eLOG_DATA_TYPE_UNSIGNED_LONG,
            eLOG_DATA_TYPE_FLOAT,
            eLOG_DATA_TYPE_DOUBLE,
            eLOG_DATA_TYPE_BOOLEAN,
            eLOG_DATA_TYPE_CHAR,
            eLOG_DATA_TYPE_STRING,
            eLOG_DATA_TYPE_DATE_TIME,
            eLOG_DATA_TYPE_LAST
        };

        public enum eLogTypes
        {
            eLOG_UNKNOWN = 0,
            eLOG_NONE,
            eLOG_SYSTEM_WAKE_UP,
            eLOG_SET_DEFAULT_REGISTRAITON_DATA,
            eLOG_START_READ_DEFAULT_REGISTRAITON_DATA,
            eLOG_FINISH_READ_DEFAULT_REGISTRAITON_DATA,
            eLOG_READ_DEFAULT_REGISTRAITON_DATA_FAILED,
            eLOG_READ_DEFAULT_REGISTRAITON_DATA_OK,
            eLOG_START_READ_REGISTRAITON_DATA,
            eLOG_FINISH_READ_REGISTRAITON_DATA,
            eLOG_READ_REGISTRAITON_DATA_FAILED,
            eLOG_READ_REGISTRAITON_DATA_OK,
            eLOG_START_PARSING_REGISTRATION_DATA,
            eLOG_FINISH_PARSING_REGISTRATION_DATA,
            eLOG_ERROR_PARSING_REGISTRATION_DATA,
            eLOG_SYSTEM_INITIALIZE,
            eLOG_START_GPS_SYNC,
            eLOG_GPS_SYNC_SPI_TIME,
            eLOG_START_GPS_SYNC_PPS,
            eLOG_SET_GPS_SYNC_TO,
            eLOG_FINISH_GPS_SYNC,
            eLOG_GPS_SYNC_FAILED,
            eLOG_GPS_SYNC_OK,
            eLOG_SET_RTC_BY_GPS_SYNC,
            eLOG_START_RECORDING,
            eLOG_IN_RECORDING,
            eLOG_CLEAR_OLD_RECORDING_DATA,
            eLOG_FINISH_RECORDING,
            eLOG_RECORDING_FAILED,
            eLOG_RECORDING_OK,
            eLOG_START_SENDING_FILE,
            eLOG_END_SENDING_FILE,
            eLOG_SENDING_FILE_FAILED,
            eLOG_SENDING_FILE_OK,
            eLOG_START_GAIN_CALIBRATION,
            eLOG_CLEAR_OLD_GAIN_CALIBRATION_DATA,
            eLOG_SET_DEFAULT_GAIN_TO_2,
            eLOG_IN_GAIN_CALIBRATION,
            eLOG_CALCULATE_GAIN_CALIBRATION,
            eLOG_FINISH_GAIN_CALIBRATION,
            eLOG_GAIN_CALIBRATION_FAILED,
            eLOG_GAIN_CALIBRATION_OK,
            eLOG_GAIN_CALIBRATION_VALUE,
            eLOG_GAIN_CALCULATED_VALUE,
            eLOG_SET_GAIN_TO_DEFAULT_VALUE,
            eLOG_GAIN_IS_FIXED_VALUE,
            eLOG_MODEM_INITIALIZED,
            eLOG_START_MODEM_OK,
            eLOG_START_MODEM_FAILED,
            eLOG_START_MODEM,
            eLOG_STOP_MODEM,
            eLOG_FTP_CHANGE_FOLDER,
            eLOG_FTP_CHANGE_FOLDER_OK,
            eLOG_FTP_CHANGE_FOLDER_FAILED,
            eLOG_FTP_OPEN_SESSION,
            eLOG_FTP_CLOSE_SESSION,
            eLOG_READ_CONFIG_DATA,
            eLOG_START_SEND_RECORD_FILES,
            eLOG_FINISH_SEND_RECORD_FILES,
            eLOG_SEND_RECORD_FILES_FAILED,
            eLOG_SEND_RECORD_FILES_OK,
            eLOG_START_SEND_LOG_FILES,
            eLOG_FINISH_SEND_LOG_FILES,
            eLOG_SEND_LOG_FILES_FAILED,
            eLOG_SEND_LOG_FILES_OK,
            eSTATUSLOG_CLEAR_OLD_LOG_FILES,
            eLOG_START_SEND_CONFIG_FILES,
            eLOG_FINISH_SEND_CONFIG_FILES,
            eLOG_SEND_CONFIG_FILES_FAILED,
            eLOG_SEND_CONFIG_FILES_OK,
            eLOG_START_FIRMWARE_UPDATE,
            eLOG_FINISH_FIRMWARE_UPDATE,
            eLOG_FIRMWARE_UPDATE_FAILED,
            eLOG_FIRMWARE_UPDATE_OK,
            eLOG_START_HARDWARE_TEST,
            eLOG_FINISH_HARDWARE_TEST,
            eLOG_HARDWARE_TEST_FAILED,
            eLOG_HARDWARE_TEST_OK,
            eLOG_START_RECORDING_WAVE_FILE,
            eLOG_FINISH_RECORDING_WAVE_FILE,
            eLOG_RECORDING_WAVE_FILE_FAILED,
            eLOG_RECORDING_WAVE_FILE_OK,
            eLOG_START_RECORDING_PRESSURE_SENSOR,
            eLOG_FINISH_RECORDING_PRESSURE_SENSOR,
            eLOG_RECORDING_PRESSURE_SENSOR_FAILED,
            eLOG_RECORDING_PRESSURE_SENSOR_OK,
            eLOG_IN_RECORDING_WAVE_FILE,
            eLOG_SET_DEFAULT_CONFIG_FILE,
            eLOG_START_READING_CONFIG_FILE,
            eLOG_FINISH_READING_CONFIG_FILE,
            eLOG_READING_CONFIG_FILE_FAILED,
            eLOG_READING_CONFIG_FILE_OK,
            eLOG_START_PARSING_CONFIG_FILE,
            eLOG_FINISH_PARSING_CONFIG_FILE,
            // for each log file need as start
            eLOG_DATE_TIME,
            eLOG_SERVER_IP_USED,
            eLOG_FIRMWARE_VERSION,
            eLOG_CONFIG_FILE_VERSION,
            eLOG_SENSOR_CONFIG_DATA,
            eLOG_GPS_POSITION_LATITUDE,
            eLOG_GPS_POSITION_LONGITUDE,
            eLOG_GPS_POSITION_ELEVATION,
            eLOG_GPS_SYNC_TYPE,
            eLOG_GPS_START_RTC_TIME,
            eLOG_GPS_END_RTC_TIME,
            eLOG_GPS_TIME_OF_SATELLITE_LOCKED,
            eLOG_GAIN_USED,
            eLOG_GAIN_AVERAGE_CALCULATED,
            eLOG_GAIN_STD_CALCULATED,
            eLOG_BATTERY_LEVEL,
            eLOG_START_TIME_RECORDING_CORRECTION_VS_GPS,
            eLOG_END_TIME_RECORDING,
            eLOG_DELTA_TIME_BETWEEN_GPS_AND_LOCAL,
            eLOG_LAST_SYNC_GPS_PPS_TIME,
            // eLOG_SENSOR_CONFIG_DATA parameters its followed the enum "ESensorConfig" if changes will be made we need to do changes here also
            eLOG_RECORD_TIME,
            eLOG_TRANSMIT_DAY,
            eLOG_START_RECORD_TIME_1,
            eLOG_START_RECORD_TIME_2,
            eLOG_START_RECORD_TIME_3,
            eLOG_SAMPLE_RATE,
            eLOG_GAIN,
            eLOG_GAIN_THRESH_HOLD_1,
            eLOG_GAIN_THRESH_HOLD_2,
            eLOG_GAIN_THRESH_HOLD_3,
            eLOG_GPS_WAKEUP_TIME_BEFORE_RECORDING,
            eLOG_GAIN_WAKEUP_TIME_BEFORE_RECORDING,
            eLOG_GAIN_SAMPLE_DURATION,
            eLOG_TRANSMIT_MAX_TIME,
            eLOG_TRANSMIT_WINDOW_START_TIME,
            eLOG_TIME_WINDOW_TO_TRANSMIT,
            eLOG_TIME_PRESSURE,
            eLOG_INTERVAL_PRESSURE,
            eLOG_TIME_PERIOD_NOISE,
            eLOG_TIME_DURATION_NOISE,
            eLOG_NOISE_THRESH_HOLD,
            eLOG_SET_RUNNING_RTC_BY_GPS_SYNC,
            eLOG_MODEM_COMMAND_TIMEOUT,
            eLOG_SIM_CCID,
            eLOG_GPS_START_TIME,
            eLOG_ALARM_WAKEUP,
            eLOG_GPS_USAGE,
            eLOG_MODEM_USAGE,
            eSTATUSLOG_NTP_SERVERREC,
            eSTATUSLOG_NTP_SERVERTRANSMIT,
            eSTATUSLOG_NTP_DEVICE_REC,
            eSTATUSLOG_NTP_DEVICETRANSMIT,
            eLOG_FIRMWARE_BUILD,
            eLOG_HW_VERSION,
            eLOG_GPS_POSITION_QUADNS,
            eLOG_GPS_POSITION_QUADEW,
            eLOG_BAT_VOLTAGE_BEFORE_RECORDING,
            eLOG_BAT_VOLTAGE_AFTER_RECORDING,
            eLOG_SETTING_DATE_FROM_GPS,
            eLOG_LAST_LOG  /* always last */
        };


        public struct LogTypesDescription
        {
            public eLogTypes eLogID;
            public string strLogDescription;

            public LogTypesDescription(eLogTypes id, string strDesc)
            {
                eLogID = id;
                strLogDescription = strDesc;
            }

        }

        LogTypesDescription[] logTypeDescription =
        {
            new LogTypesDescription(eLogTypes.eLOG_UNKNOWN, "eLOG_UNKNOWN" ),
            new LogTypesDescription(eLogTypes.eLOG_NONE, "eLOG_NONE" ),
            new LogTypesDescription(eLogTypes.eLOG_SYSTEM_WAKE_UP, "eLOG_SYSTEM_WAKE_UP" ),
            new LogTypesDescription(eLogTypes.eLOG_SET_DEFAULT_REGISTRAITON_DATA, "eLOG_SET_DEFAULT_REGISTRAITON_DATA" ),
            new LogTypesDescription(eLogTypes.eLOG_START_READ_DEFAULT_REGISTRAITON_DATA, "eLOG_START_READ_DEFAULT_REGISTRAITON_DATA" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_READ_DEFAULT_REGISTRAITON_DATA, "eLOG_FINISH_READ_DEFAULT_REGISTRAITON_DATA" ),
            new LogTypesDescription(eLogTypes.eLOG_READ_DEFAULT_REGISTRAITON_DATA_FAILED, "eLOG_READ_DEFAULT_REGISTRAITON_DATA_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_READ_DEFAULT_REGISTRAITON_DATA_OK, "eLOG_READ_DEFAULT_REGISTRAITON_DATA_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_START_READ_REGISTRAITON_DATA, "eLOG_START_READ_REGISTRAITON_DATA" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_READ_REGISTRAITON_DATA, "eLOG_FINISH_READ_REGISTRAITON_DATA" ),
            new LogTypesDescription(eLogTypes.eLOG_READ_REGISTRAITON_DATA_FAILED, "eLOG_READ_REGISTRAITON_DATA_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_READ_REGISTRAITON_DATA_OK, "eLOG_READ_REGISTRAITON_DATA_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_START_PARSING_REGISTRATION_DATA, "eLOG_START_PARSING_REGISTRATION_DATA" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_PARSING_REGISTRATION_DATA, "eLOG_FINISH_PARSING_REGISTRATION_DATA" ),
            new LogTypesDescription(eLogTypes.eLOG_ERROR_PARSING_REGISTRATION_DATA, "eLOG_ERROR_PARSING_REGISTRATION_DATA" ),
            new LogTypesDescription(eLogTypes.eLOG_SYSTEM_INITIALIZE, "eLOG_SYSTEM_INITIALIZE" ),
            new LogTypesDescription(eLogTypes.eLOG_START_GPS_SYNC, "eLOG_START_GPS_SYNC" ),
            new LogTypesDescription(eLogTypes.eLOG_GPS_SYNC_SPI_TIME, "eLOG_GPS_SYNC_SPI_TIME" ),
            new LogTypesDescription(eLogTypes.eLOG_START_GPS_SYNC_PPS, "eLOG_GPS_SYNC_PPS" ),
            new LogTypesDescription(eLogTypes.eLOG_SET_GPS_SYNC_TO, "eLOG_SET_GPS_SYNC_TO" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_GPS_SYNC, "eLOG_FINISH_GPS_SYNC" ),
            new LogTypesDescription(eLogTypes.eLOG_GPS_SYNC_FAILED, "eLOG_GPS_SYNC_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_GPS_SYNC_OK, "eLOG_GPS_SYNC_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_SET_RTC_BY_GPS_SYNC, "eLOG_SET_RTC_BY_GPS_SYNC" ),
            new LogTypesDescription(eLogTypes.eLOG_START_RECORDING, "eLOG_START_RECORDING" ),
            new LogTypesDescription(eLogTypes.eLOG_IN_RECORDING, "eLOG_IN_RECORDING"),
            new LogTypesDescription(eLogTypes.eLOG_CLEAR_OLD_RECORDING_DATA, "eLOG_CLEAR_OLD_RECORDING_DATA" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_RECORDING, "eLOG_FINISH_RECORDING" ),
            new LogTypesDescription(eLogTypes.eLOG_RECORDING_FAILED, "eLOG_RECORDING_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_RECORDING_OK, "eLOG_RECORDING_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_START_SENDING_FILE, "eLOG_START_SENDING_FILE" ),
            new LogTypesDescription(eLogTypes.eLOG_END_SENDING_FILE, "eLOG_END_SENDING_FILE" ),
            new LogTypesDescription(eLogTypes.eLOG_SENDING_FILE_FAILED, "eLOG_SENDING_FILE_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_SENDING_FILE_OK, "eLOG_SENDING_FILE_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_START_GAIN_CALIBRATION, "eLOG_START_GAIN_CALIBRATION" ),
            new LogTypesDescription(eLogTypes.eLOG_CLEAR_OLD_GAIN_CALIBRATION_DATA, "eLOG_CLEAR_OLD_GAIN_CALIBRATION_DATA"),
            new LogTypesDescription(eLogTypes.eLOG_SET_DEFAULT_GAIN_TO_2, "eLOG_SET_DEFAULT_GAIN_TO_2,"),
            new LogTypesDescription(eLogTypes.eLOG_IN_GAIN_CALIBRATION, "eLOG_IN_GAIN_CALIBRATION" ),
            new LogTypesDescription(eLogTypes.eLOG_CALCULATE_GAIN_CALIBRATION, "eLOG_CALCULATE_GAIN_CALIBRATION"),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_GAIN_CALIBRATION, "eLOG_FINISH_GAIN_CALIBRATION" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_CALIBRATION_FAILED, "eLOG_GAIN_CALIBRATION_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_CALIBRATION_OK, "eLOG_GAIN_CALIBRATION_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_CALIBRATION_VALUE, "eLOG_GAIN_CALIBRATION_VALUE" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_CALCULATED_VALUE, "eLOG_GAIN_CALCULATED_VALUE" ),
            new LogTypesDescription(eLogTypes.eLOG_SET_GAIN_TO_DEFAULT_VALUE, "eLOG_SET_GAIN_TO_DEFAULT_VALUE" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_IS_FIXED_VALUE, "eLOG_GAIN_IS_FIXED_VALUE" ),
            new LogTypesDescription(eLogTypes.eLOG_MODEM_INITIALIZED, "eLOG_MODEM_INITIALIZED" ),
            new LogTypesDescription(eLogTypes.eLOG_START_MODEM_OK, "eLOG_START_MODEM_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_START_MODEM_FAILED, "eLOG_START_MODEM_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_START_MODEM, "eLOG_START_MODEM" ),
            new LogTypesDescription(eLogTypes.eLOG_STOP_MODEM, "eLOG_STOP_MODEM" ),
            new LogTypesDescription(eLogTypes.eLOG_FTP_CHANGE_FOLDER, "eLOG_FTP_CHANGE_FOLDER" ),
            new LogTypesDescription(eLogTypes.eLOG_FTP_CHANGE_FOLDER_OK, "eLOG_FTP_CHANGE_FOLDER_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_FTP_CHANGE_FOLDER_FAILED, "eLOG_FTP_CHANGE_FOLDER_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_FTP_OPEN_SESSION, "eLOG_FTP_OPEN_SESSION" ),
            new LogTypesDescription(eLogTypes.eLOG_FTP_CLOSE_SESSION, "eLOG_FTP_CLOSE_SESSION" ),
            new LogTypesDescription(eLogTypes.eLOG_READ_CONFIG_DATA, "eLOG_READ_CONFIG_DATA" ),
            new LogTypesDescription(eLogTypes.eLOG_START_SEND_RECORD_FILES, "eLOG_START_SEND_RECORD_FILES" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_SEND_RECORD_FILES, "eLOG_FINISH_SEND_RECORD_FILES" ),
            new LogTypesDescription(eLogTypes.eLOG_SEND_RECORD_FILES_FAILED, "eLOG_SEND_RECORD_FILES_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_SEND_RECORD_FILES_OK, "eLOG_SEND_RECORD_FILES_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_START_SEND_LOG_FILES, "eLOG_START_SEND_LOG_FILES" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_SEND_LOG_FILES, "eLOG_FINISH_SEND_LOG_FILES" ),
            new LogTypesDescription(eLogTypes.eLOG_SEND_LOG_FILES_FAILED, "eLOG_SEND_LOG_FILES_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_SEND_LOG_FILES_OK, "eLOG_SEND_LOG_FILES_OK" ),
            new LogTypesDescription(eLogTypes.eSTATUSLOG_CLEAR_OLD_LOG_FILES, "eSTATUSLOG_CLEAR_OLD_LOG_FILES" ),
            new LogTypesDescription(eLogTypes.eLOG_START_SEND_CONFIG_FILES, "eLOG_START_SEND_CONFIG_FILES" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_SEND_CONFIG_FILES, "eLOG_FINISH_SEND_CONFIG_FILES" ),
            new LogTypesDescription(eLogTypes.eLOG_SEND_CONFIG_FILES_FAILED, "eLOG_SEND_CONFIG_FILES_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_SEND_CONFIG_FILES_OK, "eLOG_SEND_CONFIG_FILES_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_START_FIRMWARE_UPDATE, "eLOG_START_FIRMWARE_UPDATE" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_FIRMWARE_UPDATE, "eLOG_FINISH_FIRMWARE_UPDATE" ),
            new LogTypesDescription(eLogTypes.eLOG_FIRMWARE_UPDATE_FAILED, "eLOG_FIRMWARE_UPDATE_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_FIRMWARE_UPDATE_OK, "eLOG_FIRMWARE_UPDATE_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_START_HARDWARE_TEST, "eLOG_START_HARDWARE_TEST" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_HARDWARE_TEST, "eLOG_FINISH_HARDWARE_TEST" ),
            new LogTypesDescription(eLogTypes.eLOG_HARDWARE_TEST_FAILED, "eLOG_HARDWARE_TEST_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_HARDWARE_TEST_OK, "eLOG_HARDWARE_TEST_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_START_RECORDING_WAVE_FILE,  "eLOG_START_RECORDING_WAVE_FILE," ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_RECORDING_WAVE_FILE, "eLOG_FINISH_RECORDING_WAVE_FILE" ),
            new LogTypesDescription(eLogTypes.eLOG_RECORDING_WAVE_FILE_FAILED, "eLOG_RECORDING_WAVE_FILE_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_RECORDING_WAVE_FILE_OK, "eLOG_RECORDING_WAVE_FILE_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_START_RECORDING_PRESSURE_SENSOR, "eLOG_START_RECORDING_PRESSURE_SENSOR" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_RECORDING_PRESSURE_SENSOR, "eLOG_FINISH_RECORDING_PRESSURE_SENSOR" ),
            new LogTypesDescription(eLogTypes.eLOG_RECORDING_PRESSURE_SENSOR_FAILED, "eLOG_RECORDING_PRESSURE_SENSOR_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_RECORDING_PRESSURE_SENSOR_OK, "eLOG_RECORDING_PRESSURE_SENSOR_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_IN_RECORDING_WAVE_FILE, "eLOG_IN_RECORDING_WAVE_FILE" ),
            new LogTypesDescription(eLogTypes.eLOG_SET_DEFAULT_CONFIG_FILE, "eLOG_SET_DEFAULT_CONFIG_FILE" ),
            new LogTypesDescription(eLogTypes.eLOG_START_READING_CONFIG_FILE, "eLOG_START_READING_CONFIG_FILE" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_READING_CONFIG_FILE, "eLOG_FINISH_READING_CONFIG_FILE" ),
            new LogTypesDescription(eLogTypes.eLOG_READING_CONFIG_FILE_FAILED, "eLOG_READING_CONFIG_FILE_FAILED" ),
            new LogTypesDescription(eLogTypes.eLOG_READING_CONFIG_FILE_OK, "eLOG_READING_CONFIG_FILE_OK" ),
            new LogTypesDescription(eLogTypes.eLOG_START_PARSING_CONFIG_FILE, "eLOG_START_PARSING_CONFIG_FILE" ),
            new LogTypesDescription(eLogTypes.eLOG_FINISH_PARSING_CONFIG_FILE, "eLOG_FINISH_PARSING_CONFIG_FILE" ),
	        // for each log file need as start
	        new LogTypesDescription(eLogTypes.eLOG_DATE_TIME, "eLOG_DATE_TIME" ),
            new LogTypesDescription(eLogTypes.eLOG_SERVER_IP_USED, "eLOG_SERVER_IP_USED" ),
            new LogTypesDescription(eLogTypes.eLOG_FIRMWARE_VERSION, "eLOG_FIRMWARE_VERSION" ),
            new LogTypesDescription(eLogTypes.eLOG_CONFIG_FILE_VERSION, "eLOG_CONFIG_FILE_VERSION" ),
            new LogTypesDescription(eLogTypes.eLOG_SENSOR_CONFIG_DATA, " eLOG_SENSOR_CONFIG_DATA" ),
            new LogTypesDescription(eLogTypes.eLOG_GPS_POSITION_LATITUDE, "eLOG_GPS_POSITION_LATITUDE" ),
            new LogTypesDescription(eLogTypes.eLOG_GPS_POSITION_LONGITUDE, "eLOG_GPS_POSITION_LONGITUDE" ),
            new LogTypesDescription(eLogTypes.eLOG_GPS_POSITION_ELEVATION, "eLOG_GPS_POSITION_ELEVATION" ),
            new LogTypesDescription(eLogTypes.eLOG_GPS_SYNC_TYPE, "eLOG_GPS_SYNC_TYPE" ),
            new LogTypesDescription(eLogTypes.eLOG_GPS_START_RTC_TIME, "eLOG_GPS_START_RTC_TIME" ),
            new LogTypesDescription(eLogTypes.eLOG_GPS_END_RTC_TIME, "eLOG_GPS_END_RTC_TIME" ),
            new LogTypesDescription(eLogTypes.eLOG_GPS_TIME_OF_SATELLITE_LOCKED, "eLOG_GPS_TIME_OF_SATELLITE_LOCKED" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_USED, "eLOG_GAIN_USED" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_AVERAGE_CALCULATED, "eLOG_GAIN_AVERAGE_CALCULATED" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_STD_CALCULATED, "eLOG_GAIN_STD_CALCULATED" ),
            new LogTypesDescription(eLogTypes.eLOG_BATTERY_LEVEL, "eLOG_BATTERY_LEVEL" ),
            new LogTypesDescription(eLogTypes.eLOG_START_TIME_RECORDING_CORRECTION_VS_GPS, "eLOG_START_TIME_RECORDING" ),
            new LogTypesDescription(eLogTypes.eLOG_END_TIME_RECORDING, "eLOG_END_TIME_RECORDING" ),
            new LogTypesDescription(eLogTypes.eLOG_DELTA_TIME_BETWEEN_GPS_AND_LOCAL, "eLOG_DELTA_TIME_BETWEEN_GPS_AND_LOCAL" ),
            new LogTypesDescription(eLogTypes.eLOG_LAST_SYNC_GPS_PPS_TIME, "eLOG_LAST_SYNC_GPS_PPS_TIME"),
            new LogTypesDescription(eLogTypes.eLOG_RECORD_TIME, "eLOG_RECORD_TIME" ),
            new LogTypesDescription(eLogTypes.eLOG_TRANSMIT_DAY, "eLOG_TRANSMIT_DAY" ),
            new LogTypesDescription(eLogTypes.eLOG_START_RECORD_TIME_1, "eLOG_START_RECORD_TIME_1" ),
            new LogTypesDescription(eLogTypes.eLOG_START_RECORD_TIME_2, "eLOG_START_RECORD_TIME_2" ),
            new LogTypesDescription(eLogTypes.eLOG_START_RECORD_TIME_3, "eLOG_START_RECORD_TIME_3" ),
            new LogTypesDescription(eLogTypes.eLOG_SAMPLE_RATE, "eLOG_SAMPLE_RATE" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN, "eLOG_GAIN" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_THRESH_HOLD_1, "eLOG_GAIN_THRESH_HOLD_1" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_THRESH_HOLD_2, "eLOG_GAIN_THRESH_HOLD_2" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_THRESH_HOLD_3, "eLOG_GAIN_THRESH_HOLD_3" ),
            new LogTypesDescription(eLogTypes.eLOG_GPS_WAKEUP_TIME_BEFORE_RECORDING, "eLOG_GPS_WAKEUP_TIME_BEFORE_RECORDING" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_WAKEUP_TIME_BEFORE_RECORDING, "eLOG_GAIN_WAKEUP_TIME_BEFORE_RECORDING" ),
            new LogTypesDescription(eLogTypes.eLOG_GAIN_SAMPLE_DURATION, "eLOG_GAIN_SAMPLE_DURATION" ),
            new LogTypesDescription(eLogTypes.eLOG_TRANSMIT_MAX_TIME, "eLOG_TRANSMIT_MAX_TIME" ),
            new LogTypesDescription(eLogTypes.eLOG_TRANSMIT_WINDOW_START_TIME, "eLOG_TRANSMIT_WINDOW_START_TIME" ),
            new LogTypesDescription(eLogTypes.eLOG_TIME_WINDOW_TO_TRANSMIT, "eLOG_TIME_WINDOW_TO_TRANSMIT" ),
            new LogTypesDescription(eLogTypes.eLOG_TIME_PRESSURE, "eLOG_TIME_PRESSURE" ),
            new LogTypesDescription(eLogTypes.eLOG_INTERVAL_PRESSURE, "eLOG_INTERVAL_PRESSURE" ),
            new LogTypesDescription(eLogTypes.eLOG_TIME_PERIOD_NOISE, "eLOG_TIME_PERIOD_NOISE" ),
            new LogTypesDescription(eLogTypes.eLOG_TIME_DURATION_NOISE, "eLOG_TIME_DURATION_NOISE" ),
            new LogTypesDescription(eLogTypes.eLOG_NOISE_THRESH_HOLD, "eLOG_NOISE_THRESH_HOLD" ),
            new LogTypesDescription(eLogTypes.eLOG_SET_RUNNING_RTC_BY_GPS_SYNC, "eLOG_SET_RUNNING_RTC_BY_GPS_SYNC" ),
            new LogTypesDescription(eLogTypes.eLOG_MODEM_COMMAND_TIMEOUT, "eLOG_MODEM_COMMAND_TIMEOUT"),
            new LogTypesDescription(eLogTypes.eLOG_SIM_CCID, "eLOG_SIM_CCID"),
            new LogTypesDescription(eLogTypes.eLOG_GPS_START_TIME,"eLog_GPS_START_TIME"),
            new LogTypesDescription(eLogTypes.eLOG_ALARM_WAKEUP,"eLOG_ALARM_WAKEUP"),
            new LogTypesDescription(eLogTypes.eLOG_GPS_USAGE, "eLOG_GPS_USAGE"),
            new LogTypesDescription(eLogTypes.eLOG_MODEM_USAGE,"eLOG_MODEM_USAGE"),
            new LogTypesDescription(eLogTypes.eSTATUSLOG_NTP_SERVERREC,"eSTATUSLOG_NTP_SERVERREC"),
            new LogTypesDescription(eLogTypes.eSTATUSLOG_NTP_SERVERTRANSMIT,"eSTATUSLOG_NTP_SERVERTRANSMIT"),
            new LogTypesDescription(eLogTypes.eSTATUSLOG_NTP_DEVICE_REC, "eSTATUSLOG_NTP_DEVICE_REC"),
            new LogTypesDescription(eLogTypes.eSTATUSLOG_NTP_DEVICETRANSMIT,"eSTATUSLOG_NTP_DEVICETRANSMIT"),
            new LogTypesDescription(eLogTypes.eLOG_FIRMWARE_BUILD,"eLOG_FIRMWARE_BUILD"),
            new LogTypesDescription(eLogTypes.eLOG_HW_VERSION,"eLOG_HW_VERSION"),
            new LogTypesDescription(eLogTypes.eLOG_GPS_POSITION_QUADNS,"eLOG_GPS_POSITION_QUADNS"),
            new LogTypesDescription(eLogTypes.eLOG_GPS_POSITION_QUADEW,"eLOG_GPS_POSITION_QUADEW"),
            new LogTypesDescription(eLogTypes.eLOG_BAT_VOLTAGE_BEFORE_RECORDING,"eLOG_BAT_VOLTAGE_BEFORE_RECORDING"),
            new LogTypesDescription(eLogTypes.eLOG_BAT_VOLTAGE_AFTER_RECORDING,"eLOG_BAT_VOLTAGE_AFTER_RECORDING"),
        new LogTypesDescription(eLogTypes.eLOG_SETTING_DATE_FROM_GPS,"eLOG_SETTING_DATE_FROM_GPS"),
        };

        public LogFile()
        {
            logData.Clear();
        }

        /// <summary>
        /// Reads log data from a file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public byte[] ReadLogData(String filename)
        {
            byte[] abyData = null;
            if (filename.Length > 0)
            {
                FileInfo fileInfo = new FileInfo(filename);
                FileDataFields fileData = new FileDataFields();
                fileData.SetDefault();
                fileData.sFilename = fileInfo.Name;
                fileData.fIsLogFile = false;
                SplitFileName.SplitFilenames(ref fileData);

                logData.sModemID = fileData.sUnitName;
                //s.SplitFileName(filename);
                LogFileType = fileData.eLogType;
                FileStream stream = new FileStream(filename, FileMode.Open);
                BinaryReader reader = new BinaryReader(stream);
                abyData = new byte[stream.Length]; //set to size of file
                reader.Read(abyData, 0, (int)stream.Length);
                if (LogEvent != null)
                {
                    LogEvent("\nReading log file: " + Path.GetFileName(filename));
                }
                stream.Close();
            }
            return abyData;
        }

        public void ClearNTPList()
        {
            listNTPData.Clear();
        }

        public ELogFileType LogFileType
        {
            get { return eLogFileType; }
            set { eLogFileType = value; }
        }

        /// <summary>
        /// Print the NTP data to screen and to the log file with the same name as the log file with extension ".csv"
        /// </summary>
        /// <param name="sFilename"></param>
        public void PrintNTPList(string sFilename)
        {
            string sNTPDat = "";
            int iCount = listNTPData.Count / 4;
            ushort us6Bits;
            ushort us10Bits;
            if (listNTPData.Count > 0)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sFilename))
                    {
                        sNTPDat = "Server Receive, Seconds, Fraction, Server Transmit, Seconds, Fraction, Device Receive, Seconds, Fraction, Device Transmit, Seconds, Fraction";
                        if (LogEvent != null)
                        {
                            LogEvent(sNTPDat);
                        }
                        sw.WriteLine(sNTPDat);
                        for (int i = 0; i < iCount; i++)
                        {
                            sNTPDat = "";
                            for (int j = 0; j < 4; j++)
                            {
                                us6Bits = listNTPData[i * 4 + j];
                                us6Bits >>= 10;
                                us10Bits = listNTPData[i * 4 + j];
                                us10Bits &= 0x3FF;
                                sNTPDat += Convert.ToString(listNTPData[i * 4 + j]) + " ,";
                                sNTPDat += Convert.ToString(us6Bits) + " ,";
                                sNTPDat += Convert.ToString(us10Bits) + ((j < 3) ? " ," : "");
                            }
                            //sNTPDat += "\n";
                            if (LogEvent != null)
                            {
                                LogEvent(sNTPDat);
                            }
                            sw.WriteLine(sNTPDat);
                        }
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    sNTPDat = "Could not write the file : [ " + sFilename + " ]\n";
                    if (LogEvent != null)
                    {
                        LogEvent(sNTPDat);
                    }
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Get the NTP data list
        /// </summary>
        public List<ushort> NTPListData
        {
            get { return listNTPData; }
        }

        public List<string> LogData
        {
            get { return lstLogData; }
        }

        public void ParseOldLogFile(byte[] abyReadBuffer)
        {
            lstLogData.Clear();
            int iCurrentPos = 0;
            ELogExtendedDataTypes eLogDataType;
            byte bDataSize;
            int iLogNumBytes;
            if (abyReadBuffer != null)
            {
                while ((int)(iCurrentPos + sizeof(byte) + sizeof(uint) + sizeof(byte)) <= abyReadBuffer.Length)
                {
                    eLogDataType = (ELogExtendedDataTypes)abyReadBuffer[iCurrentPos + sizeof(byte) + sizeof(uint)];
                    bDataSize = GetExtraLogDataSize(eLogDataType, abyReadBuffer, iCurrentPos + sizeof(byte) + sizeof(uint) + sizeof(byte));
                    if ((int)(iCurrentPos + sizeof(byte) + sizeof(int) + sizeof(byte) + bDataSize) <= abyReadBuffer.Length)
                    {
                        iLogNumBytes = ParseSingleLog(abyReadBuffer, iCurrentPos);
                        if (iLogNumBytes == -1)
                        {
                            break;
                        }
                        iCurrentPos += iLogNumBytes;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void ParseLogFile(byte[] abyReadBuffer, String filename)
        {
            if (LogFileType == ELogFileType.eLOG_TYPE_NEW_FORMAT)
            {
                LogFileParser2 logFileNewFormat = new LogFileParser2();
                logFileNewFormat.SplitFullFilenames(filename);
                logFileNewFormat.ParseLogFile(abyReadBuffer);
                lstLogData = logFileNewFormat.LogData;
                string sModeMID = logData.sModemID; // save it we will overwrite it
                logData = logFileNewFormat.logData;
                logData.sModemID = sModeMID;
            }
            else
            {
                if (LogFileType == ELogFileType.eLOG_FILE_OLD_FORMAT)
                {
                    ParseOldLogFile(abyReadBuffer);
                }
            }
        }

        public void ParseLogFile(String filename)
        {
            //lstLogData.Clear();
            byte[] abyReadBuffer;
            abyReadBuffer = ReadLogData(filename);
            ParseLogFile(abyReadBuffer, filename);
        }

        private byte GetExtraLogDataSize(ELogExtendedDataTypes eLogDataType, byte[] abyData, int iIndex)
        {
            byte bDataSize = 0;

            switch (eLogDataType)
            {
                case ELogExtendedDataTypes.eLOG_DATA_TYPE_NONE:
                    bDataSize = 0;
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_BYTE:
                    bDataSize = sizeof(byte);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_INT:
                    bDataSize = sizeof(Int16);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_LONG:
                    bDataSize = sizeof(uint);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_UNSIGNED_SHORT:
                    bDataSize = sizeof(ushort);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_UNSIGNED_INT:
                    bDataSize = sizeof(UInt16);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_UNSIGNED_LONG:
                    bDataSize = sizeof(uint);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_DATE_TIME:
                    bDataSize = sizeof(uint);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_FLOAT:
                    bDataSize = sizeof(float);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_DOUBLE:
                    bDataSize = sizeof(double);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_BOOLEAN:
                    bDataSize = sizeof(bool);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_CHAR:
                    bDataSize = 1; //chars here are 2 bytes, on embedded side they are 1 byte
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_STRING:
                    do
                    {
                        bDataSize++;
                    }
                    while (abyData[iIndex++] != 0);
                    if (bDataSize > MAX_LOG_DATA_SIZE)
                    {
                        bDataSize = MAX_LOG_DATA_SIZE;
                    }
                    break;

                default:
                    bDataSize = 0; // never get to here
                    break;
            }

            return bDataSize;
        }

        public static string GetVerison(string sFWVersion)
        {
            string[] sarVersion = sFWVersion.Split('.');
            string sMinorVersion = "";
            string sMajorVersion = "";
            string sVersion = SplitFileName.FIRMWARE_VERSION_UNKNOWN;

            try
            {
                sMinorVersion = Convert.ToInt32(sarVersion[1]).ToString();
                sMajorVersion = Convert.ToInt32(sarVersion[0]).ToString();
                sVersion = sMajorVersion + "." + sMinorVersion;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return sVersion;
        }

        public static string NMEAGPSLocationToGMapCoordinates(string nmeaCoord)
        {
            if ((nmeaCoord.Length < 3) || (!nmeaCoord.Contains(".")))
            {
                return ""; //there isnt valid data to begin with
            }

            int decPointIndex;
            double degrees, minutes, seconds, DD;
            try
            {
                //the format is DDMM.SS (degrees, minutes, seconds)
                decPointIndex = nmeaCoord.IndexOf('.'); //12345.67
                if (decPointIndex > 2)
                {
                    degrees = Double.Parse(nmeaCoord.Substring(0, decPointIndex - 2)); //get the degrees
                    minutes = Double.Parse(nmeaCoord.Substring(decPointIndex - 2, 2)); //get the minutes
                    string s = nmeaCoord.Substring(decPointIndex + 1, nmeaCoord.Length - decPointIndex - 1);
                    seconds = double.Parse("0." + s);
                    seconds = 60.0 * seconds;
                    DD = degrees + (minutes / 60) + (seconds / 3600);
                    return DD.ToString();
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(e);
                throw ex;
            }
            return "";
        }


        private void FIllTheLogDataInfo(eLogTypes eLogID, string strLogMessageDataString)
        {
            try
            {
                switch (eLogID)
                {
                    case eLogTypes.eLOG_SET_GPS_SYNC_TO:
                        logData.iGPSSyncType = Convert.ToInt32(strLogMessageDataString);
                        break;

                    case eLogTypes.eLOG_GPS_POSITION_LATITUDE:
                        {
                            string sConvert = NMEAGPSLocationToGMapCoordinates(strLogMessageDataString);
                            if (sConvert.Length > 0)
                            {
                                logData.floatGPSPosLatitude = (float)Convert.ToDouble(sConvert);
                            }
                            else
                            {
                                logData.floatGPSPosLatitude = (float)Convert.ToDouble(strLogMessageDataString);
                            }
                        }
                        break;

                    case eLogTypes.eLOG_GPS_POSITION_QUADNS:
                        logData.sloatGPSPosQuadns = strLogMessageDataString;
                        break;

                    case eLogTypes.eLOG_GPS_POSITION_LONGITUDE:
                        {
                            string sConvert = NMEAGPSLocationToGMapCoordinates(strLogMessageDataString);
                            if (sConvert.Length > 0)
                            {
                                logData.floatGPSPosLongitude = (float)Convert.ToDouble(sConvert);
                            }
                            else
                            {
                                logData.floatGPSPosLongitude = (float)Convert.ToDouble(strLogMessageDataString);
                            }
                        }
                        break;

                    case eLogTypes.eLOG_GPS_POSITION_QUADEW:
                        logData.sloatGPSPosQuadew = strLogMessageDataString;
                        break;

                    case eLogTypes.eLOG_GPS_POSITION_ELEVATION:
                        logData.floatGPSPosElevation = (float)Convert.ToDouble(strLogMessageDataString);
                        break;

                    case eLogTypes.eLOG_SIM_CCID:
                        {
                            string[] sarAny = strLogMessageDataString.Split(':');
                            if (sarAny.Length < 1)
                            {
                                logData.sSimCcid = strLogMessageDataString;
                            }
                            else
                            {
                                sarAny = sarAny[1].Split('\0');
                                logData.sSimCcid = sarAny[0].Trim();
                            }
                        }
                        break;

                    case eLogTypes.eLOG_GAIN_USED:
                        logData.iGainUsed = Convert.ToInt32(strLogMessageDataString);
                        break;

                    case eLogTypes.eLOG_HW_VERSION:
                        logData.sHardwareVersion = strLogMessageDataString;
                        break;

                    case eLogTypes.eLOG_FIRMWARE_BUILD:
                        logData.sFirmwareBuild = strLogMessageDataString;
                        break;

                    case eLogTypes.eLOG_FIRMWARE_VERSION:
                        logData.sFirmwareVerison = GetVerison(strLogMessageDataString);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private int ParseSingleLog(byte[] abyReadBuffer, int iStartIndex)
        {
            string strLogMessageString;
            string strLogMessageDataString;
            int iCurrentPos = iStartIndex;
            eLogTypes eLogID;
            ELogExtendedDataTypes eLogDataType;
            ulong ulTimeStamp;
            int iNumBytes = 0;
            byte bDataSize;
            int iVal;
            ulong ulVal;
            uint uiVal;
            ushort usVal = 0;
            long lVal;
            float fVal;
            bool bVal;
            char cVal;

            ulTimeStamp = (ulong)BitConverter.ToInt32(abyReadBuffer, iCurrentPos);
            iCurrentPos += sizeof(uint);
            eLogID = (eLogTypes)abyReadBuffer[iCurrentPos];
            iCurrentPos += sizeof(byte);
            eLogDataType = (ELogExtendedDataTypes)abyReadBuffer[iCurrentPos];
            iCurrentPos += sizeof(byte);
            iNumBytes = sizeof(uint) + sizeof(byte) + sizeof(byte);
            if ((eLogDataType < (byte)ELogExtendedDataTypes.eLOG_DATA_TYPE_NONE) || (((byte)eLogDataType >= (byte)ELogExtendedDataTypes.eLOG_DATA_TYPE_LAST)) ||
                ((eLogID < (byte)eLogTypes.eLOG_UNKNOWN) || ((byte)eLogID >= (byte)eLogTypes.eLOG_LAST_LOG)))
            {
                // unknown data type so ignored the entry
                return -1;
            }
            bDataSize = GetExtraLogDataSize(eLogDataType, abyReadBuffer, iCurrentPos);
            iNumBytes += bDataSize;

            switch (eLogDataType)
            {
                case ELogExtendedDataTypes.eLOG_DATA_TYPE_NONE:
                    strLogMessageDataString = "\0";
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_BYTE:
                    iVal = abyReadBuffer[iCurrentPos];
                    strLogMessageDataString = iVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_INT:
                    iVal = BitConverter.ToInt32(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = iVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_LONG:
                    lVal = BitConverter.ToInt32(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = lVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_UNSIGNED_SHORT:
                    usVal = BitConverter.ToUInt16(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = usVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_UNSIGNED_INT:
                    uiVal = BitConverter.ToUInt32(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = uiVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_UNSIGNED_LONG:
                    ulVal = BitConverter.ToUInt32(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = ulVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_DATE_TIME:
                    ulVal = BitConverter.ToUInt32(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = GetDateTimeString(ulVal);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_FLOAT:
                    fVal = (float)BitConverter.ToSingle(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = fVal.ToString("0.0000");
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_DOUBLE:
                    fVal = (float)BitConverter.ToSingle(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = fVal.ToString("0.0000");
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_BOOLEAN:
                    bVal = BitConverter.ToBoolean(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = bVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_CHAR:
                    cVal = (char)abyReadBuffer[iCurrentPos];
                    strLogMessageDataString = cVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_STRING:
                    strLogMessageDataString = System.Text.Encoding.UTF8.GetString(abyReadBuffer, iCurrentPos, bDataSize);
                    break;

                default:
                    strLogMessageDataString = "";
                    break;
            }
            /// add the data to the list assumong we have four entry for each in the order as in the if List below
            if ((eLogID == eLogTypes.eSTATUSLOG_NTP_DEVICE_REC) ||
                (eLogID == eLogTypes.eSTATUSLOG_NTP_DEVICETRANSMIT) ||
                (eLogID == eLogTypes.eSTATUSLOG_NTP_SERVERREC) ||
                (eLogID == eLogTypes.eSTATUSLOG_NTP_SERVERTRANSMIT))
            {
                listNTPData.Add(usVal);
            }

            FIllTheLogDataInfo(eLogID, strLogMessageDataString);

            strLogMessageString = GetDateTimeString(ulTimeStamp);
            strLogMessageString += "  -  ";
            strLogMessageString += logTypeDescription[(byte)eLogID].strLogDescription;
            if (strLogMessageDataString.Length > 0 && strLogMessageDataString != "\0")
            {
                strLogMessageString += "  -  ";
                strLogMessageString += strLogMessageDataString;
            }
            strLogMessageString = strLogMessageString.Trim('\0'); // '/0' is not a null terminator in c#
            if (LogEvent != null)
            {
                LogEvent(strLogMessageString);
            }

            lstLogData.Add(strLogMessageString);

            return iNumBytes;
        }

        string GetDateTimeString(ulong ulVal)
        {
            System.DateTime dt = new System.DateTime(1970, 1, 1).AddSeconds(ulVal);
            return dt.ToString();
        }
    }
}
