using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

/*************************************************************************
*                 New Log format with miliseconds data from version 1.71
*				  TimeStamp - 4 bytes for time
*                 MiliSeconds TimeStamp - 2 bytes for time
*				  EID - Enumarated ID log type
*				  DID - Enumarated Log Data ID log data type
*				  LDA - Log Data - the actual data and the size is depend on the written size up to 50 bytes of data mainly for text
*				  NUL - Null character
*                 -----------------------------------------------------------------------------------------------------
* Bytes position  ...| 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |10 |11 |12 |13 |14 |15 |16 |17 |18 |19 |20 |.......
*				  ---|---------------|---|---|---|---|-------------------|---------------|---|---|---|---|-------------
* Actual log Data ...| 02| 13| 0E| D0| 12|4F | 12|10 | A | R | I | E |NUL|02 |13 | 0D| 9 |34 |78 |02 | 0 |.......
*				  ---|---------------|---|---|---|---|-------------------|---------------|---|---|---|---|-------------
* Log Descripion  ...| Time stamp    |milSec |EID|DID| LDA               | Time stamp    |milSec |EID|DID|.......
*				  ---|---------------|---|---|---|---|-------------------|---------------|---|---|---|---|-------------
* Logic Logs Num. ---|-------- Log Number N -----------------------------|---- Log Number N+1 -----------|----------------------------
* 
* *************************************************************************/

namespace Prod3Common
{
    public class LogFileParser2
    {


        public delegate void LogEventDelegate(string text);
        public event LogEventDelegate LogEvent;
        private const int MAX_LOG_DATA_SIZE = 50;	// eqch log cannot be more then 50 chars
        private int iLogsize = sizeof(uint) + sizeof(UInt16) + sizeof(byte) + sizeof(UInt16) + sizeof(byte);
        public List<string> lstLogData = new List<string>();
        public LogFileData logData = new LogFileData();

        /// <summary>
        /// Quick and Dirty to get all parameters that has extra data attached to it
        /// /////////// global parameters for getting to third part applicaiotn //////////////////////////
        /// </summary>
        /// 



        /// <summary>
        /// 
        /// </summary>
        private enum eLogType
        {
            eLOG_TYPE_EVENT,
            eLOG_TYPE_STATUS
        }

        /// <summary>
        /// 
        /// </summary>
        private enum ELogExtendedDataTypes
        {
            eLOG_DATA_TYPE_NONE = 0,
            eLOG_DATA_TYPE_BYTE,
            eLOG_DATA_TYPE_SHORT,
            eLOG_DATA_TYPE_UNSIGNED_SHORT,
            eLOG_DATA_TYPE_INT32,
            eLOG_DATA_TYPE_UNSIGNED_INT32,
            eLOG_DATA_TYPE_FLOAT,
            eLOG_DATA_TYPE_DOUBLE,
            eLOG_DATA_TYPE_BOOLEAN,
            eLOG_DATA_TYPE_CHAR,
            eLOG_DATA_TYPE_STRING,
            eLOG_DATA_TYPE_DATE_TIME,
            eLOG_DATA_TYPE_LAST
        };

        public enum eEventLog
        {
            eEVENTLOG_NONE,
            eEVENTLOG_SYSTEM_START_UP,
            eEVENTLOG_START_GAIN_CALIBRATION,
            eEVENTLOG_CALCULATING_GAIN_CALIBRATION,
            eEVENTLOG_CALCULATED_STD_GAIN_CALIBRATION,
            eEVENTLOG_GAIN_IS_FIXED_VALUE,
            eEVENTLOG_FINISHED_GAIN_CALIBRATION,
            eEVENTLOG_SET_GAIN_TO_DEFAULT_VALUE,
            eEVENTLOG_FINISHED_RECORDING_WAVE_FILE,
            eEVENTLOG_START_SENDING_WAVE_FILES,
            eEVENTLOG_CLEARING_OLD_GAIN_CALIBRATION_DATA,
            eEVENTLOG_CLEARING_OLD_RECORDING_DATA,
            eEVENTLOG_CLEARING_OLD_LOG_FILES,
            eEVENTLOG_OPENING_MODEM,
            eEVENTLOG_OPEN_MODEM_FAILED,
            eEVENTLOG_CLOSE_MODEM_FAILED,
            eEVENTLOG_MODEM_OPENED_OK,
            eEVENTLOG_MODEM_CLOSED_OK,
            eEVENTLOG_BATTERY_TOO_LOW_FOR_MODEM,
            eEVENTLOG_CLOSING_MODEM,
            eEVENTLOG_FTP_CONNECTED,
            eEVENTLOG_FTP_CONNECT_FAILED,
            eEVENTLOG_START_SENDING_LOG_FILES,
            eEVENTLOG_START_GPS_SYNC,
            eEVENTLOG_FINISHED_GPS_SYNC,
            eEVENTLOG_GOT_PPS,
            eEVENTLOG_GPS_SYNC_FAILED,
            eEVENTLOG_ALARM_WAKEUP,
            eEVENTLOG_SET_RUNNING_RTC_BY_GPS_SYNC,
            eEVENTLOG_MODEM_COMMAND_TIMEOUT,
            eEVENTLOG_GPS_SYNC_SPI_TIME,
            eEVENTLOG_SETTING_DATE_FROM_GPS,
            eEVENTLOG_START_READ_REGISTRAITON_DATA,
            eEVENTLOG_READ_REGISTRAITON_DATA_OK,
            eEVENTLOG_READ_REGISTRAITON_DATA_FAILED,
            eEVENTLOG_FINISH_READ_REGISTRAITON_DATA,
            eEVENTLOG_START_READ_DEFAULT_REGISTRAITON_DATA,
            eEVENTLOG_READ_DEFAULT_REGISTRAITON_DATA_OK,
            eEVENTLOG_FINISH_READ_DEFAULT_REGISTRAITON_DATA,
            eEVENTLOG_READ_DEFAULT_REGISTRAITON_DATA_FAILED,
            eEVENTLOG_START_PARSING_CONFIG_FILE,
            eEVENTLOG_FINISH_PARSING_CONFIG_FILE,
            eEVENTLOG_START_PARSING_REGISTRATION_DATA,
            eEVENTLOG_ERROR_PARSING_REGISTRATION_DATA,
            eEVENTLOG_FINISH_PARSING_REGISTRATION_DATA,
            eEVENTLOG_START_READING_CONFIG_FILE,
            eEVENTLOG_READING_CONFIG_FILE_OK,
            eEVENTLOG_READING_CONFIG_FILE_FAILED,
            eEVENTLOG_FINISH_READING_CONFIG_FILE,
            eEVENTLOG_MODEM_COMMAND_FAILED,
            eEVENTLOG_MODEM_SIGNAL_STRENGTH,
            eEVENTLOG_START_NOISE_GAIN_CALIBRATION,
            eEVENTLOG_RADIO_LOCK_CHANNEL_STATUS,
            eEVENTLOG_RADIO_LOCK_CHANNEL_NUMBER,
            eEVENTLOG_RADIO_LOCK_CHANNEL_SNR,
            eEVENTLOG_RADIO_MODEM_DATE_TIME_READ,
            eEVENTLOG_FINISHED_RECORDING_RADIO_WAVE_FILE,
        }

        public LogFileParser2()
        {
            logData.Clear();
        }

        public List<string> LogData
        {
            get { return lstLogData; }
        }

        private string getEventLogString(eEventLog log, string dataString)
        {
            //switch case with all the text
            string str = "";
            switch (log)
            {
                case eEventLog.eEVENTLOG_NONE:
                    str = "No Event";
                    break;
                case eEventLog.eEVENTLOG_SYSTEM_START_UP:
                    str = "System Startup";
                    break;
                case eEventLog.eEVENTLOG_START_GAIN_CALIBRATION:
                    str = "Starting gain calibration";
                    break;
                case eEventLog.eEVENTLOG_CALCULATING_GAIN_CALIBRATION:
                    str = "Calculating gain";
                    break;

                case eEventLog.eEVENTLOG_CALCULATED_STD_GAIN_CALIBRATION:
                    str = "Calculating gain STD " + dataString;
                    logData.m_sCalculatingGainSTD = dataString;
                    break;

                case eEventLog.eEVENTLOG_GAIN_IS_FIXED_VALUE:
                    str = "Using fixed gain value";
                    break;
                case eEventLog.eEVENTLOG_FINISHED_GAIN_CALIBRATION:
                    str = "Finished gain calibration";
                    break;
                case eEventLog.eEVENTLOG_SET_GAIN_TO_DEFAULT_VALUE:
                    str = "Gain out of bounds - using default value";
                    break;
                case eEventLog.eEVENTLOG_FINISHED_RECORDING_WAVE_FILE:
                    str = "Finished recording Wave File";
                    break;
                case eEventLog.eEVENTLOG_START_SENDING_WAVE_FILES:
                    str = "Started to send Wave files";
                    break;
                case eEventLog.eEVENTLOG_CLEARING_OLD_GAIN_CALIBRATION_DATA:
                    str = "Clearing old gain data";
                    break;
                case eEventLog.eEVENTLOG_CLEARING_OLD_RECORDING_DATA:
                    str = "Clearing old recorded audio data";
                    break;
                case eEventLog.eEVENTLOG_CLEARING_OLD_LOG_FILES:
                    str = "Clearing old log file data";
                    break;
                case eEventLog.eEVENTLOG_OPENING_MODEM:
                    str = "Opening modem";
                    break;
                case eEventLog.eEVENTLOG_OPEN_MODEM_FAILED:
                    str = "Failed to open modem";
                    break;
                case eEventLog.eEVENTLOG_CLOSE_MODEM_FAILED:
                    str = "Failed to close modem";
                    break;
                case eEventLog.eEVENTLOG_MODEM_OPENED_OK:
                    str = "Modem opened OK after " + dataString + " tries";
                    logData.m_sModemOpenedOKAfterNumRetries = dataString;
                    break;
                case eEventLog.eEVENTLOG_MODEM_CLOSED_OK:
                    str = "Modem closed OK after " + dataString + " tries";
                    logData.m_sModemClosedOKAfterNumRetries = dataString;
                    break;
                case eEventLog.eEVENTLOG_BATTERY_TOO_LOW_FOR_MODEM:
                    str = "Battery too low to open modem. Voltage = " + dataString;
                    logData.m_sBatteryVoltageTooLowToOpenModem = dataString;
                    break;
                case eEventLog.eEVENTLOG_CLOSING_MODEM:
                    str = "Closing the modem";
                    break;
                case eEventLog.eEVENTLOG_FTP_CONNECTED:
                    str = "FTP connected OK after " + dataString + " tries";
                    logData.m_sFTPConnectedOKAfterNumRetries = dataString;
                    break;
                case eEventLog.eEVENTLOG_FTP_CONNECT_FAILED:
                    str = "FTP failed to connect " + dataString + " tries";
                    logData.m_sFTPConnectedFailedAfterNumRetries = dataString;
                    break;
                case eEventLog.eEVENTLOG_START_SENDING_LOG_FILES:
                    str = "Starting to send log files";
                    break;
                case eEventLog.eEVENTLOG_START_GPS_SYNC:
                    str = "Starting GPS";
                    break;
                case eEventLog.eEVENTLOG_FINISHED_GPS_SYNC:
                    str = "Finished GPS synchronization";
                    break;
                case eEventLog.eEVENTLOG_GOT_PPS:
                    str = "PPS Pulse received with " + dataString + " satalites";
                    logData.m_sPPSPulseReceivedWithNumSatelites = dataString;
                    break;
                case eEventLog.eEVENTLOG_GPS_SYNC_FAILED:
                    str = "Failed to synchronize with GPS";
                    break;
                case eEventLog.eEVENTLOG_ALARM_WAKEUP:
                    str = "Device woken by alarm";
                    break;
                case eEventLog.eEVENTLOG_SET_RUNNING_RTC_BY_GPS_SYNC:
                    str = "Set RTC Clock by the Modem On System Startup";
                    break;
                case eEventLog.eEVENTLOG_MODEM_COMMAND_TIMEOUT:
                    str = "Modem command timed out on command " + dataString;
                    logData.m_sModemCommandTimedoutOnCommand = dataString;
                    break;
                case eEventLog.eEVENTLOG_GPS_SYNC_SPI_TIME:
                    str = "Syncronized with GPS SPI time with " + dataString + " satalites";
                    logData.m_sSyncronizedWithGPSSPITimeWithNumSatelites = dataString;
                    break;
                case eEventLog.eEVENTLOG_SETTING_DATE_FROM_GPS:
                    str = "Setting date from GPS: " + dataString;
                    logData.m_sSettingDateFromGPS = dataString;
                    break;
                case eEventLog.eEVENTLOG_START_READ_REGISTRAITON_DATA:
                    str = "Starting to read device registration data";
                    break;
                case eEventLog.eEVENTLOG_READ_REGISTRAITON_DATA_OK:
                    str = "Registration data read OK";
                    break;
                case eEventLog.eEVENTLOG_READ_REGISTRAITON_DATA_FAILED:
                    str = "Registration data reading failed";
                    break;
                case eEventLog.eEVENTLOG_FINISH_READ_REGISTRAITON_DATA:
                    str = "Finished reading registration data";
                    break;
                case eEventLog.eEVENTLOG_START_READ_DEFAULT_REGISTRAITON_DATA:
                    str = "Reading default registration data";
                    break;
                case eEventLog.eEVENTLOG_READ_DEFAULT_REGISTRAITON_DATA_FAILED:
                    str = "Failed to read default registration data";
                    break;
                case eEventLog.eEVENTLOG_READ_DEFAULT_REGISTRAITON_DATA_OK:
                    str = "Default registration data read OK";
                    break;
                case eEventLog.eEVENTLOG_FINISH_READ_DEFAULT_REGISTRAITON_DATA:
                    str = "Finished reading default registration data";
                    break;
                case eEventLog.eEVENTLOG_START_PARSING_CONFIG_FILE:
                    str = "Config data parsing started";
                    break;
                case eEventLog.eEVENTLOG_FINISH_PARSING_CONFIG_FILE:
                    str = "Finished parsing config data";
                    break;
                case eEventLog.eEVENTLOG_START_PARSING_REGISTRATION_DATA:
                    str = "Started to parse registration data";
                    break;
                case eEventLog.eEVENTLOG_ERROR_PARSING_REGISTRATION_DATA:
                    str = "Failed to parse registration data";
                    break;
                case eEventLog.eEVENTLOG_FINISH_PARSING_REGISTRATION_DATA:
                    str = "Finished parsing registration data";
                    break;
                case eEventLog.eEVENTLOG_START_READING_CONFIG_FILE:
                    str = "Started to read configuration data";
                    break;
                case eEventLog.eEVENTLOG_READING_CONFIG_FILE_OK:
                    str = "Configuration data read OK";
                    break;
                case eEventLog.eEVENTLOG_READING_CONFIG_FILE_FAILED:
                    str = "Reading config data failed";
                    break;
                case eEventLog.eEVENTLOG_FINISH_READING_CONFIG_FILE:
                    str = "Finished reading config file";
                    break;
                case eEventLog.eEVENTLOG_MODEM_COMMAND_FAILED:
                    str = "Modem Command Failed: " + dataString;
                    logData.m_sModemCommandFailed = dataString;
                    break;
                case eEventLog.eEVENTLOG_MODEM_SIGNAL_STRENGTH:
                    str = "Modem Atenna signal strength: " + dataString;
                    logData.m_sModemAtennaSignalStrength = dataString;
                    break;
                case eEventLog.eEVENTLOG_START_NOISE_GAIN_CALIBRATION:
                    str = "Start Noise Gain Calibration: ";
                    break;

                case eEventLog.eEVENTLOG_RADIO_LOCK_CHANNEL_STATUS:
                    str = "Radio Lock Channel status : " + ((dataString.Contains("1") == true) ? "Pass OK" : "Failed");
                    logData.m_sRadioLockChannelStatus = dataString;
                    break;

                case eEventLog.eEVENTLOG_RADIO_LOCK_CHANNEL_NUMBER:
                    str = "Radio Lock channel (frequency) number : " + dataString;
                    logData.m_sRadioLockChannelFrequencyNumber = dataString;
                    break;

                case eEventLog.eEVENTLOG_RADIO_LOCK_CHANNEL_SNR:
                    str = "Radio Lock channel SNR : " + dataString;
                    logData.m_sRadioLockChannelSNR = dataString;
                    break;

                case eEventLog.eEVENTLOG_RADIO_MODEM_DATE_TIME_READ:
                    str = "Radio Set time from Modem : " + ((dataString.Contains("1") == true) ? "Pass OK" : "Failed");
                    logData.m_sRadioSetTimeFromModem = dataString;
                    break;

                case eEventLog.eEVENTLOG_FINISHED_RECORDING_RADIO_WAVE_FILE:
                    str = "Finished recording Radio channel file ";
                    break;

                default:
                    str = "Unrecognized event";
                    break;
            }
            return str;
        }

        public enum eStatusLog
        {
            eSTATUSLOG_NONE,
            eSTATUSLOG_DATE_TIME,
            eSTATUSLOG_FIRMWARE_BUILD,
            eSTATUSLOG_FIRMWARE_VERSION,
            eSTATUSLOG_HW_VERSION,
            eSTATUSLOG_SERVER_IP_USED,
            eSTATUSLOG_CONFIG_FILE_VERSION,
            //config
            eSTATUSLOG_RECORD_TIME,
            eSTATUSLOG_TRANSMIT_DAY,
            eSTATUSLOG_START_RECORD_TIME_1,
            eSTATUSLOG_START_RECORD_TIME_2,
            eSTATUSLOG_START_RECORD_TIME_3,
            eSTATUSLOG_SAMPLE_RATE,
            eSTATUSLOG_GAIN,
            eSTATUSLOG_GAIN_THRESH_HOLD_1,
            eSTATUSLOG_GAIN_THRESH_HOLD_2,
            eSTATUSLOG_GAIN_THRESH_HOLD_3,
            eSTATUSLOG_GPS_WAKEUP_TIME_BEFORE_RECORDING,
            eSTATUSLOG_GAIN_WAKEUP_TIME_BEFORE_RECORDING,
            eSTATUSLOG_GAIN_SAMPLE_DURATION,
            eSTATUSLOG_TRANSMIT_MAX_TIME,
            eSTATUSLOG_TRANSMIT_WINDOW_START_TIME,
            eSTATUSLOG_TIME_WINDOW_TO_TRANSMIT,
            eSTATUSLOG_TIME_PRESSURE,
            eSTATUSLOG_INTERVAL_PRESSURE,
            eSTATUSLOG_TIME_PERIOD_NOISE,
            eSTATUSLOG_TIME_DURATION_NOISE,
            eSTATUSLOG_NOISE_THRESH_HOLD,
            //system info
            eSTATUSLOG_GPS_POSITION_LATITUDE,
            eSTATUSLOG_GPS_POSITION_QUADNS,
            eSTATUSLOG_GPS_POSITION_LONGITUDE,
            eSTATUSLOG_GPS_POSITION_QUADEW,
            eSTATUSLOG_GPS_POSITION_ELEVATION,
            eSTATUSLOG_GPS_SYNC_TYPE,
            eSTATUSLOG_GPS_START_RTC_TIME,
            eSTATUSLOG_GPS_END_RTC_TIME,
            eSTATUSLOG_GPS_TIME_OF_SATELLITE_LOCKED,
            eSTATUSLOG_GAIN_USED,
            eSTATUSLOG_GAIN_AVERAGE_CALCULATED,
            eSTATUSLOG_GAIN_STD_CALCULATED,
            eSTATUSLOG_START_TIME_RECORDING_CORRECTION_VS_GPS,
            eSTATUSLLOG_END_TIME_RECORDING,
            eSTATUSLLOG_DELTA_TIME_BETWEEN_GPS_AND_LOCAL,
            eSTATUSLLOG_LAST_SYNC_GPS_PPS_TIME,
            eSTATUSLLOG_SIM_CCID,
            eSTATUSLLOG_GPS_USAGE,
            eSTATUSLLOG_MODEM_USAGE,
            eSTATUSLLOG_BAT_VOLTAGE_BEFORE_RECORDING,
            eSTATUSLLOG_BAT_VOLTAGE_AFTER_RECORDING,
            eSTATUSLLOG_TEMPERATURE,
            // more options for Sending FTP files
            eSTATUSLLOG_FTP_START_SENDING_FILE,
            eSTATUSLLOG_FTP_OPEN_SEND_FILE_PASS,
            eSTATUSLLOG_FTP_OPEN_SEND_FILE_FAILED,
            eSTATUSLLOG_FTP_OPEN_SEND_FILE_HEADER,
            eSTATUSLLOG_FTP_SEND_FILE_SIZE,
            eSTATUSLLOG_FTP_SEND_NUMBER_BUFFERS,
            eSTATUSLLOG_FTP_SEND_NUMBER_BUFFERS_ZERO_LENGTH,
            eSTATUSLLOG_FTP_SEND_FAIED_ON_BUFFER_NUMBER,
            eSTATUSLLOG_FTP_FINISH_SENDING_FILE,
            eSTATUSLLOG_REGISTRAITON_FILE_NAME,
            eSTATUSLLOG_CONFIG_FILE_NAME,
            eSTATUSLLOG_LAST_RESET_ID_NUMBER,
            eSTATUSLLOG_LAST_SYSTEM_RESET_CAUSE_BY,
        }

        public enum ESystemResetBy
        {
            eRESET_UNKNOWN,             // reset cause by unknown issue or at start up
            eRESET_CLEARED,             // after generating the reset and handled the cause we will cleared the reser cause
            eRESET_BY_MAGNET,           // When user put magnet to generates reset
            eRESET_BY_SOFTWARE,         // when something go wrong
            eRESET_BY_UI,               // Reset cause by UI terminal outside command reset
            eRESET_BY_FIRMWARE_UPDATES, // Reset cause by new firmware
            eRESET_FAIL_TO_READ_CONFIG_AT_START_UP, // Reset because we failed toread the config on start up
            eRESET_BY_CONFIG_FILE_RESET_ID,   // Reset required by the config file Reset ID
            eRESET_BY_CORRUPTED_CONFIG_DATA     // Reset required by the corrupted config file data
        }

        private string GetSystemResetCause(string sSystemResetCauseID)
        {
            string sSystemResetBy = "unknown";
            try
            {
                switch ((ESystemResetBy)(Convert.ToByte(sSystemResetCauseID)))
                {
                    case ESystemResetBy.eRESET_UNKNOWN:
                        sSystemResetBy = "Unknown";
                        break;

                    case ESystemResetBy.eRESET_CLEARED:
                        sSystemResetBy = "Cleared";
                        break;

                    case ESystemResetBy.eRESET_BY_MAGNET:
                        sSystemResetBy = "Magnet";
                        break;

                    case ESystemResetBy.eRESET_BY_SOFTWARE:
                        sSystemResetBy = "Software";
                        break;

                    case ESystemResetBy.eRESET_BY_UI:
                        sSystemResetBy = "User UI Terminal";
                        break;

                    case ESystemResetBy.eRESET_BY_FIRMWARE_UPDATES:
                        sSystemResetBy = "Firmware Updates";
                        break;

                    case ESystemResetBy.eRESET_FAIL_TO_READ_CONFIG_AT_START_UP:
                        sSystemResetBy = "Error Reading Config at Start up - on next recording cycle";
                        break;

                    case ESystemResetBy.eRESET_BY_CONFIG_FILE_RESET_ID:
                        sSystemResetBy = "Config file Reset ID";
                        break;

                    case ESystemResetBy.eRESET_BY_CORRUPTED_CONFIG_DATA:
                        sSystemResetBy = "Corrupted Config file Data Reset ID";
                        break;

                    default:
                        break;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sSystemResetBy;
        }

        private string getStatusLogString(eStatusLog log, string dataString, string tTimeString)
        {
            //switch case with all the text
            string str = "";
            switch (log)
            {
                case eStatusLog.eSTATUSLOG_NONE:
                    str = "No Status";
                    break;
                case eStatusLog.eSTATUSLOG_DATE_TIME:
                    str = "Log date and time: " + dataString;
                    logData.m_sLogDateTime = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_FIRMWARE_BUILD:
                    str = "Firmware Build Type: " + dataString;
                    logData.m_sFirmwareBuildType = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_FIRMWARE_VERSION:
                    str = "Firmware Version:" + dataString;
                    logData.m_sFirmwareVersion = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_HW_VERSION:
                    str = "Hardware Version: " + dataString;
                    logData.m_sHardwareVersion = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_SERVER_IP_USED:
                    str = "Server IP: " + dataString;
                    logData.m_sServerIP = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_CONFIG_FILE_VERSION:
                    str = "Config File Version: " + dataString;
                    logData.m_sConfigFileVersion = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_RECORD_TIME:
                    str = "Recording Time: " + dataString;
                    logData.m_sRecordingTime = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_TRANSMIT_DAY:
                    str = "Transmit Day: " + dataString;
                    logData.m_sTransmitDay = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_START_RECORD_TIME_1:
                    str = "Recording Time 1: " + dataString;
                    logData.m_sRecordingTime1 = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_START_RECORD_TIME_2:
                    str = "Recording Time 2: " + dataString;
                    logData.m_sRecordingTime2 = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_START_RECORD_TIME_3:
                    str = "Recording Time 3: " + dataString;
                    logData.m_sRecordingTime3 = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_SAMPLE_RATE:
                    str = "Sampling Rate: " + dataString;
                    logData.m_sSamplingRate = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GAIN:
                    str = "Gain Used: " + dataString;
                    logData.m_sGainDefine = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GAIN_THRESH_HOLD_1:
                    str = "Gain Threshold 1: " + dataString;
                    logData.m_sGainThreshold1 = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GAIN_THRESH_HOLD_2:
                    str = "Gain Threshold 2: " + dataString;
                    logData.m_sGainThreshold2 = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GAIN_THRESH_HOLD_3:
                    str = "Gain Threshold 3: " + dataString;
                    logData.m_sGainThreshold3 = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GPS_WAKEUP_TIME_BEFORE_RECORDING:
                    str = "GPS Wakeup Time Before Recording: " + dataString;
                    logData.m_sGPSWakeupTimeBeforeRecording = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GAIN_WAKEUP_TIME_BEFORE_RECORDING:
                    str = "Gain Wakeup Time Before Recording: " + dataString; ;
                    logData.m_sGainWakeupTimeBeforeRecording = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GAIN_SAMPLE_DURATION:
                    str = "Gain Calculation Duration: " + dataString;
                    logData.m_sGainCalculationDuration = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_TRANSMIT_MAX_TIME:
                    str = "FTP Retries Numbers (1 - 3, other = 1 Retry): " + dataString;
                    logData.m_sFTPRetriesNumbers = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_TRANSMIT_WINDOW_START_TIME:
                    str = "Transmit Window Start Time: " + dataString;
                    logData.m_sTransmitWindowStartTime = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_TIME_WINDOW_TO_TRANSMIT:
                    str = "Transmit Window Start Time Modulo Number: " + dataString;
                    logData.m_sTransmitWindowStartTimeModuloNumber = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_TIME_PRESSURE:
                    str = "Reset ID Number (1 = No Reset): " + dataString;
                    logData.m_sResetIDNumber = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_INTERVAL_PRESSURE:
                    str = "Run Full Cycle After Reset (1 = Run, other no Run): " + dataString;
                    logData.m_sRunFullCycleAfterReset = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_TIME_PERIOD_NOISE:
                    str = "Noise Gain Calibration Time Cycle Duration: " + dataString; // NOISE_GAIN_CALIBRATION_TIME_CYCLE_DURATION
                    logData.m_sNoiseGainCalibrationTimeCycleDuration = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_TIME_DURATION_NOISE: // NOISE_GAIN_CALIBRATION_TIME_SAMPLE_DURATION
                    str = "Noise Gain Calibration Time Sample Duration: " + dataString;
                    logData.m_sNoiseGainCalibrationTimeSampleDuration = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_NOISE_THRESH_HOLD: // 
                    str = "Noise Gain Calibration Wakeup Time Before Recording: " + dataString;
                    logData.m_sNoiseGainCalibrationWakeupTimeBeforeRecording = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GPS_POSITION_LATITUDE:
                    str = "GPS Latitude: " + dataString;
                    logData.m_sGPSLatitude = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GPS_POSITION_QUADNS:
                    str = "GPS Quad N/S: " + dataString;
                    logData.m_sGPSQuadN_S = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GPS_POSITION_LONGITUDE:
                    str = "GPS Longitude: " + dataString;
                    logData.m_sGPSLongitude = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GPS_POSITION_QUADEW:
                    str = "GPS Quad E/W: " + dataString;
                    logData.m_sGPSQuadE_W = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GPS_POSITION_ELEVATION:
                    str = "GPS Elevation: " + dataString;
                    logData.m_sGPSElevation = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GPS_SYNC_TYPE:
                    str = "GPS Sync Type: " + dataString;
                    logData.m_sGPSSyncType = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GPS_START_RTC_TIME:
                    str = "GPS Start RTC Time: " + dataString;
                    logData.m_sGPSStartRTCTime = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GPS_END_RTC_TIME:
                    str = "GPS End RTC Time: " + dataString;
                    logData.m_sGPSEndRTCTime = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GPS_TIME_OF_SATELLITE_LOCKED:
                    str = "GPS Satellite Lock Time: " + dataString;
                    logData.m_sGPSSatelliteLockTime = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GAIN_USED:
                    str = "Gain Used: " + dataString;
                    logData.m_sGainUsed = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GAIN_AVERAGE_CALCULATED:
                    str = "Gain Average Calculated: " + dataString;
                    logData.m_sGainAverageCalculated = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_GAIN_STD_CALCULATED:
                    str = "STD Calculated: " + dataString;
                    logData.m_sGainSTDCalculated = dataString;
                    break;
                case eStatusLog.eSTATUSLOG_START_TIME_RECORDING_CORRECTION_VS_GPS:
                    str = "Start Recording Time Correction VS GPS: " + dataString;
                    logData.m_sStartRecordingTimeCorrectionVSGPS = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_END_TIME_RECORDING:
                    str = "End Recording Time: " + dataString;
                    logData.m_sEndRecordingTime = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_DELTA_TIME_BETWEEN_GPS_AND_LOCAL:
                    str = "Delta Time Between GPS and Local: " + dataString;
                    logData.m_sDeltaTimeBetweenGPSAndLocal = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_LAST_SYNC_GPS_PPS_TIME:
                    str = "Last Sync GPS PPS Time: " + dataString;
                    logData.m_sLastSyncGPSPPSTime = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_SIM_CCID:
                    {
                        str = "SIM CCID: " + dataString;
                        string[] sarAny = dataString.Split(':'); // strip the "#ccid:"
                        if (sarAny.Length > 1)
                        {
                            sarAny = sarAny[1].Split('\0');
                            dataString = sarAny[0].Trim();
                        }
                        logData.m_sSIMCCID = dataString;
                    }
                    break;
                case eStatusLog.eSTATUSLLOG_GPS_USAGE:
                    str = "GPS Usage: " + dataString;
                    logData.m_sGPSUsage = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_MODEM_USAGE:
                    str = "Modem Usage: " + dataString;
                    logData.m_sModemUsage = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_BAT_VOLTAGE_BEFORE_RECORDING:
                    str = "Battery Voltage Before Recording: " + dataString;
                    logData.m_sBatteryVoltageBeforeRecording = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_BAT_VOLTAGE_AFTER_RECORDING:
                    str = "Battery Voltage After Recording: " + dataString;
                    logData.m_sBatteryVoltageAfterRecording = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_TEMPERATURE:
                    str = "Unit Temperature: " + dataString;
                    logData.m_sUnitTemperature = dataString;
                    break;

                case eStatusLog.eSTATUSLLOG_FTP_START_SENDING_FILE:
                    str = "Start Sending FTP File: " + tTimeString;
                    logData.m_sStartSendingFTPFile = tTimeString;
                    break;
                case eStatusLog.eSTATUSLLOG_FTP_OPEN_SEND_FILE_PASS:
                    str = "FTP Open File [ " + dataString + " ] Pass OK";
                    logData.m_sFTPOpenOKFileName = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_FTP_OPEN_SEND_FILE_FAILED:
                    str = "FTP Open File [ " + dataString + " ] Failed";
                    logData.m_sFTPOpenFailedFileName = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_FTP_OPEN_SEND_FILE_HEADER:
                    str = "Send Wave Header File - " + ((Convert.ToInt32(dataString) == 1) ? "Pass OK" : "Failed");
                    logData.m_sSendWaveHeaderFileStatus = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_FTP_SEND_FILE_SIZE:
                    str = "FTP File size = " + dataString;
                    logData.m_sFTPFileSize = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_FTP_SEND_NUMBER_BUFFERS:
                    str = "Send File number buffers =  " + dataString;
                    logData.m_sSendFileNumberBuffers = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_FTP_SEND_NUMBER_BUFFERS_ZERO_LENGTH:
                    str = "Send File Buffer Zero length, buffer index = " + dataString;
                    logData.m_sSendFileBufferZerolengthBufferIndex = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_FTP_SEND_FAIED_ON_BUFFER_NUMBER:
                    str = "FTP Failed Send buffer index = " + dataString;
                    logData.m_sFTPFailedSendBufferIndex = dataString;
                    break;
                case eStatusLog.eSTATUSLLOG_FTP_FINISH_SENDING_FILE:
                    str = "Fininsing Sending FTP file: " + tTimeString;
                    logData.m_sFininsingSendingFTPFile = tTimeString;
                    break;

                case eStatusLog.eSTATUSLLOG_REGISTRAITON_FILE_NAME:
                    str = "Registration Filename: " + dataString;
                    logData.m_sRegistrationFilename = dataString;
                    break;

                case eStatusLog.eSTATUSLLOG_CONFIG_FILE_NAME:
                    str = "Configuration Filename: " + dataString;
                    logData.m_sConfigurationFilename = dataString;
                    break;

                case eStatusLog.eSTATUSLLOG_LAST_RESET_ID_NUMBER:
                    str = "Last Reset ID Number : " + dataString;
                    logData.m_sLastResetIDNumber = dataString;
                    break;

                case eStatusLog.eSTATUSLLOG_LAST_SYSTEM_RESET_CAUSE_BY:
                    str = "Reset Issue due to : " + GetSystemResetCause(dataString);
                    logData.m_sResetIssueIDNumber = dataString;
                    break;

                default:
                    str = "Unrecognized Status ";
                    break;
            }
            return str;
        }




        /// <summary>
        /// Reads log data from a file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private byte[] ReadLogData(String filename)
        {
            byte[] abyData = null;
            if (filename.Length > 0)
            {
                FileStream stream = new FileStream(filename, FileMode.Open);
                BinaryReader reader = new BinaryReader(stream);
                abyData = new byte[stream.Length]; //set to size of file
                reader.Read(abyData, 0, (int)stream.Length);
                if (LogEvent != null)
                    LogEvent("\nReading log file: " + Path.GetFileName(filename));
                stream.Close();
            }
            return abyData;
        }

        public void ParseLogFile(byte[] abyReadBuffer)
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
                    eLogDataType = (ELogExtendedDataTypes)abyReadBuffer[iCurrentPos + sizeof(short) + sizeof(uint)];
                    bDataSize = GetExtraLogDataSize(eLogDataType, abyReadBuffer, iCurrentPos + iLogsize);
                    if ((int)(iCurrentPos + iLogsize + bDataSize) <= abyReadBuffer.Length)
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

        public void ParseLogFile(String filename)
        {
            byte[] abyReadBuffer;
            abyReadBuffer = ReadLogData(filename);
            ParseLogFile(abyReadBuffer);
            SplitFullFilenames(filename);
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

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_SHORT:
                    bDataSize = sizeof(Int16);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_UNSIGNED_SHORT:
                    bDataSize = sizeof(Int16);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_UNSIGNED_INT32:
                case ELogExtendedDataTypes.eLOG_DATA_TYPE_INT32:
                    bDataSize = sizeof(UInt32);
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_DATE_TIME:
                    bDataSize = sizeof(UInt32);
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

        private void FIllTheLogDataInfo(eStatusLog eLogID, string strLogMessageDataString)
        {
            try
            {
                switch (eLogID)
                {
                    case eStatusLog.eSTATUSLOG_GPS_SYNC_TYPE:
                        logData.iGPSSyncType = Convert.ToInt32(strLogMessageDataString);
                        break;

                    case eStatusLog.eSTATUSLOG_GPS_POSITION_LATITUDE:
                        {
                            string sConvert = LogFile.NMEAGPSLocationToGMapCoordinates(strLogMessageDataString);
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

                    case eStatusLog.eSTATUSLOG_GPS_POSITION_QUADNS:
                        logData.sloatGPSPosQuadns = strLogMessageDataString;
                        break;

                    case eStatusLog.eSTATUSLOG_GPS_POSITION_LONGITUDE:
                        {
                            string sConvert = LogFile.NMEAGPSLocationToGMapCoordinates(strLogMessageDataString);
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

                    case eStatusLog.eSTATUSLOG_GPS_POSITION_QUADEW:
                        logData.sloatGPSPosQuadew = strLogMessageDataString;
                        break;

                    case eStatusLog.eSTATUSLOG_GPS_POSITION_ELEVATION:
                        logData.floatGPSPosElevation = (float)Convert.ToDouble(strLogMessageDataString);
                        break;

                    case eStatusLog.eSTATUSLLOG_SIM_CCID:
                        {
                            string[] sarAny = strLogMessageDataString.Split(':');
                            if (sarAny.Length < 1)
                            {
                                logData.sSimCcid = strLogMessageDataString;
                            }
                            else
                            {
                                logData.sSimCcid = sarAny[1].Trim();
                            }
                        }
                        break;

                    case eStatusLog.eSTATUSLOG_GAIN_USED:
                        logData.iGainUsed = Convert.ToInt32(strLogMessageDataString);
                        break;

                    case eStatusLog.eSTATUSLOG_HW_VERSION:
                        logData.sHardwareVersion = strLogMessageDataString;
                        break;

                    case eStatusLog.eSTATUSLOG_FIRMWARE_BUILD:
                        logData.sFirmwareBuild = strLogMessageDataString;
                        break;

                    case eStatusLog.eSTATUSLOG_FIRMWARE_VERSION:
                        logData.sFirmwareVerison = LogFile.GetVerison(strLogMessageDataString);
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
            eEventLog eEventLogNum = eEventLog.eEVENTLOG_NONE;
            eStatusLog eStatusLogNum = eStatusLog.eSTATUSLOG_NONE;
            eLogType logType;
            ELogExtendedDataTypes eLogDataType;
            ulong ulTimeStamp;
            UInt16 usTimeStampMs;
            int iNumBytes = 0;
            byte bDataSize;
            Int32 iVal;
            UInt32 uiVal;
            ulong ulVal;
            UInt16 usVal = 0;
            Int16 sVal = 0;
            float fVal;
            bool bVal;
            char cVal;

            //timestamp
            ulTimeStamp = (ulong)BitConverter.ToInt32(abyReadBuffer, iCurrentPos);
            iCurrentPos += sizeof(uint);
            //timestamp ms
            usTimeStampMs = (UInt16)BitConverter.ToInt16(abyReadBuffer, iCurrentPos);
            iCurrentPos += sizeof(UInt16);
            //log type
            logType = (eLogType)abyReadBuffer[iCurrentPos];
            iCurrentPos += sizeof(byte);
            //log number
            if (logType == eLogType.eLOG_TYPE_EVENT)
            {
                eEventLogNum = (eEventLog)BitConverter.ToInt16(abyReadBuffer, iCurrentPos);
            }
            else
            {
                eStatusLogNum = (eStatusLog)BitConverter.ToInt16(abyReadBuffer, iCurrentPos);
            }
            iCurrentPos += sizeof(UInt16);
            //log data type
            eLogDataType = (ELogExtendedDataTypes)abyReadBuffer[iCurrentPos];
            iCurrentPos += sizeof(byte);
            //
            iNumBytes = iLogsize;
            if ((eLogDataType < (byte)ELogExtendedDataTypes.eLOG_DATA_TYPE_NONE) || (((byte)eLogDataType >= (byte)ELogExtendedDataTypes.eLOG_DATA_TYPE_LAST)))
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

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_SHORT:
                    sVal = BitConverter.ToInt16(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = sVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_UNSIGNED_SHORT:
                    usVal = BitConverter.ToUInt16(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = usVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_INT32:
                    iVal = (Int32)BitConverter.ToInt32(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = iVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_UNSIGNED_INT32:
                    uiVal = (UInt32)BitConverter.ToInt32(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = uiVal.ToString();
                    break;

                case ELogExtendedDataTypes.eLOG_DATA_TYPE_DATE_TIME:
                    ulVal = BitConverter.ToUInt32(abyReadBuffer, iCurrentPos);
                    strLogMessageDataString = GetDateTimeString(ulVal, 0);
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
                    strLogMessageDataString = "Unknown data type";
                    break;
            }

            if (logType == eLogType.eLOG_TYPE_STATUS)
            {
                FIllTheLogDataInfo(eStatusLogNum, strLogMessageDataString);
            }

            string tLogDateTime = GetDateTimeString(ulTimeStamp, usTimeStampMs);
            strLogMessageString = tLogDateTime + "  -  ";
            if (logType == eLogType.eLOG_TYPE_EVENT)
                strLogMessageString += getEventLogString(eEventLogNum, strLogMessageDataString);
            if (logType == eLogType.eLOG_TYPE_STATUS)
                strLogMessageString += getStatusLogString(eStatusLogNum, strLogMessageDataString, tLogDateTime);
            strLogMessageString = strLogMessageString.Replace('\0', ' '); // '/0' is not a null terminator in c#
            if (LogEvent != null)
                LogEvent(strLogMessageString);

            lstLogData.Add(strLogMessageString);

            return iNumBytes;
        }

        string GetDateTimeString(ulong ulVal, int ms)
        {
            string dateString;
            try
            {
                System.DateTime dt = new System.DateTime(1970, 1, 1).AddSeconds(ulVal).AddMilliseconds(ms);
                dateString = dt.ToString("dd/MM/yy HH:mm:ss:fff");
            }
            catch
            {
                Debug.WriteLine("Time parsing error: Seconds = " + ulVal);
                dateString = "Time Error:";
            }
            return dateString;
        }

        public void SplitFullFilenames(string sFullFilename)
        {
            logData.m_sUnitName = "";
            logData.m_sTransmitTime = "";

            FileInfo fileInfo = new FileInfo(sFullFilename);
            FileDataFields fileData = new FileDataFields();
            fileData.SetDefault();
            fileData.sFilename = fileInfo.Name;
            fileData.fIsLogFile = true;
            SplitFileName.SplitFilenames(ref fileData);
            logData.m_sUnitName = fileData.sUnitName;
            logData.m_sTransmitTime = fileData.sTransmitTime;
        }
    }
}
