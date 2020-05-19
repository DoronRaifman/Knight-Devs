using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prod3Common
{
    public enum ELogFileType
    {
        eLOG_FILE_TYPE_UNKNOWN,
        eLOG_FILE_OLD_FORMAT,
        eLOG_TYPE_NEW_FORMAT
    }

    public enum EFileType
    {
        eUNKNOWN_FILE_TYPE,
        eLOG_FILE_TYPE,
        eWAV_FILE_TYPE,
        eALARM_FILE_TYPE,
        ePRESSURE_SENSOR_FILE_TYPE,
        eCONFIG_FILE_TYPE,
        eFIRMWARE_FILE_TYPE,
        eREGISTRATION_FILE_TYPE,
    }

    /// <summary>
    /// 
    /// </summary>
    public struct FileDataFields
    {
        public string sUnitName;
        public string sUnitVersion;
        public string sDate;
        public string sFilename;
        public string sGPS;
        public string sTransmitTime;
        public string sSize;
        public string sGainUsed;
        public int nSyncDuration;
        public string sHardwareVersion;
        public string sSimCcid;
        public int iIndex; // index of that entry in the list
        public bool fIsLogFile;
        public bool fLogAlreadyRead;
        public ELogFileType eLogType;
        public EFileType eFileType;
        public float fBatteryVoltageBeforeRecording;
        public float fBatteryVoltageAfterRecording;

        /// <summary>
        /// 
        /// </summary>
        public void SetDefault()
        {
            sUnitName = "";
            sUnitVersion = "";
            sDate = "";
            sFilename = "";
            sGPS = "";
            sTransmitTime = "";
            sSize = "";
            sGainUsed = "";
            nSyncDuration = 0;
            sHardwareVersion = "";
            sSimCcid = "";
            iIndex = 0;
            fIsLogFile = false;
            fLogAlreadyRead = false;
            eLogType = ELogFileType.eLOG_FILE_TYPE_UNKNOWN;
            eFileType = EFileType.eUNKNOWN_FILE_TYPE;
            fBatteryVoltageBeforeRecording = 0;
            fBatteryVoltageAfterRecording = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileDataFields"></param>
        public FileDataFields(FileDataFields fileDataFields)
        {
            sUnitName = fileDataFields.sUnitName;
            sUnitVersion = fileDataFields.sUnitVersion;
            sDate = fileDataFields.sDate;
            sFilename = fileDataFields.sFilename;
            sGPS = fileDataFields.sGPS;
            sTransmitTime = fileDataFields.sTransmitTime;
            sSize = fileDataFields.sSize;
            sGainUsed = fileDataFields.sGainUsed;
            nSyncDuration = fileDataFields.nSyncDuration;
            sHardwareVersion = fileDataFields.sHardwareVersion;
            sSimCcid = fileDataFields.sSimCcid;
            iIndex = fileDataFields.iIndex;
            fIsLogFile = fileDataFields.fIsLogFile;
            fLogAlreadyRead = fileDataFields.fLogAlreadyRead;
            eLogType = fileDataFields.eLogType;
            eFileType = fileDataFields.eFileType;
            fBatteryVoltageBeforeRecording = fileDataFields.fBatteryVoltageBeforeRecording;
            fBatteryVoltageAfterRecording = fileDataFields.fBatteryVoltageAfterRecording;
        }
    };

    public class SplitFileName
    {
        public const string FIRMWARE_VERSION_UNKNOWN = "x.xx";

        // old log file name format
        // ========================
        // auto_<Modem#>_<Day>_<Month>_<Year>_<Hour>_<Minutes>_<Seconds>_log.bindat
        // auto_354678050252464_17_11_2015_15_24_00_log.bindat
        public const string LOG_FILE_NAME_OLD_EXTENSION = "_log.bindat";

        // log file name format
        // ========================
        // auto_<Modem#>_<Day>_<Month>_<Year>_<Hour>_<Minutes>_<Seconds>.log
        // auto_354678050252464_17_11_2015_15_24_00.log
        public const string LOG_FILE_NAME_NEW_EXTENSION = ".log"; // auto_354678050252464_17_11_2015_15_24_00.log
        public const string LOG_FILE_NAME_NEW_PREFIX = "auto"; // auto_354678050252464_17_11_2015_15_24_00.log
        public const string RADIO_LOG_FILE_NAME_NEW_PREFIX = "autoR"; // autoR_354678050252464_17_11_2015_15_24_00.log
        public const string TEST_LOG_FILE_NAME_NEW_PREFIX = "Test"; // Test_354678051333768_22_02_2016_15_45_00.log
        public const string RADIO_TEST_LOG_FILE_NAME_NEW_PREFIX = "TestR"; // TestR_354678051333768_22_02_2016_15_45_00.log

        // wave file name format
        //===================================
        // auto_<Modem#>_<Day>_<Month>_<Year>_<Hour>_<Minutes>_<Seconds>_<GPS Sync Type>_<Gain>_<Pps Drift Test Time>_<Pps Ticks>_<Pps Record Time Difference>_<Min Noise Gain STD Calculated Gain * 10>_<STD Calculated Gain * 10>_sig.wav
        // auto_354678051333768_22_02_2016_15_45_00_2_4_0_0_1962380_0_24_sig.wav
        public const string WAVE_FILE_NAME_EXTENSION = "_sig.wav"; //Test_354678051333768_22_02_2016_15_45_00_2_4_0_0_1962380_0_24_sig.wav
        public const string WAVE_FILE_NAME_PREFIX = "auto";//auto_354678051333768_22_02_2016_15_45_00_2_4_0_0_1962380_0_24_sig.wav
        public const string TEST_WAVE_FILE_NAME_PREFIX = "Test"; //Test_354678051333768_22_02_2016_15_45_00_2_4_0_0_1962380_0_24_sig.wav

        // Radio wave file name format
        //===================================
        // autoR_<Modem#>_<Day>_<Month>_<Year>_<Hour>_<Minutes>_<Seconds>_<GPS Sync Type>_<Gain>_<Radio Recording Time>_<Pps Drift Test Time>_<Pps Ticks>_<Pps Record Time Difference>_<Min Noise Gain STD Calculated Gain * 10>_<STD Calculated Gain * 10>_sig.wav
        // autoR_354678051333768_22_02_2016_15_45_00_2_4_5_0_0_1962380_0_24_sig.wav
        public const string RADIO_WAVE_FILE_NAME_PREFIX = "autoR"; //autoR_354678051333768_22_02_2016_15_45_00_2_4_5_0_0_1962380_0_24_sig.wav
        public const string RADIO_TEST_WAVE_FILE_NAME_PREFIX = "TestR"; //TestR_354678051333768_22_02_2016_15_45_00_2_4_5_0_0_1962380_0_24_sig.wav

        // pressure sensor file name format
        //===================================
        // Trendp<Modem#>_<Day>_<Month>_<Year>_<Hour>_<Minutes>_<Seconds>_<miliseconds>.bin
        // Trendp_354678051272263_22_02_2016_13_18_35_000.bin;
        public const string PRESSURE_SENSOR_FILE_NAME_EXTENSION = ".bin"; //Trendp_354678051272263_22_02_2016_13_18_35_000.bin;
        public const string PRESSURE_SENSOR_FILE_NAME_PREFIX = "Trendp"; //Trendp_354678051272263_22_02_2016_13_18_35_000.bin;
        public const string PRESSURE_SENSOR_HIT_FILE_NAME_PREFIX = "Transient"; //Trendp_354678051272263_22_02_2016_13_18_35_000.bin;

        // alarm file name format
        //===================================
        // Alarm_<Modem#>_<Day>_<Month>_<Year>_<Hour>_<Minutes>_<Seconds>_<miliseconds>_<Number of Hiting up to sendig the file>.txt
        // Alarm_359658044010082_20_12_2015_23_34_30_000_6.txt
        public const string ALARM_FILE_NAME_EXTENSION = ".txt"; // Alarm_359658044010082_20_12_2015_23_34_30_000_9.txt
        public const string ALARM_FILE_NAME_PREFIXPREFIX = "Alarm"; // Alarm_359658044010082_20_12_2015_23_34_30_000_9.txt

        // config file name format
        //===================================
        // config_<Modem#>.bindat
        // config_359658044010082.bindat
        public const string CONFIG_FILE_NAME_PREFIX = "config_";
        public const string CONFIG_FILE_NAME_EXTENSION = ".bindat";

        // registration file name format
        //===================================
        // reg_<Modem#>.bindat
        // reg_359658044010082.bindat
        public const string REGISTRATION_FILE_NAME_PREFIX = "reg_";
        public const string REGISTRATION_FILE_NAME_EXTENSION = ".bindat";

        // firmware file name format
        //===================================
        // firmware_<Minor>_<Major>.bin
        // firmware_2_89.bin
        public const string FIRMWARE_FILE_NAME_PREFIX = "firmware";
        public const string FIRMWARE_FILE_NAME_EXTENSION = "bin";

        /// <summary>
        /// 
        /// </summary>
        public enum EGPSType
        {
            eGPS_NONE = -1,
            eGPS_TYPE_RTC = 0,
            eGPS_TYPE_SPI,
            eGPS_TYPE_PPS
        }

        /// <summary>
        /// 
        /// </summary>
        private static FileDataFields m_fileData;

        /// <summary>
        /// 
        /// </summary>
        public SplitFileName()
        {
            m_fileData = new FileDataFields();
            m_fileData.SetDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sGPS"></param>
        /// <returns></returns>
        public static string GetGPSType(string sGPS)
        {
            string sGPSType = "N.A";

            try
            {
                EGPSType eGPStype = (EGPSType)Convert.ToInt32(sGPS);
                switch (eGPStype)
                {
                    case EGPSType.eGPS_TYPE_RTC:
                        sGPSType = "RTC";
                        break;

                    case EGPSType.eGPS_TYPE_SPI:
                        sGPSType = "SPI";
                        break;

                    case EGPSType.eGPS_TYPE_PPS:
                        sGPSType = "PPS";
                        break;

                    default:
                        sGPSType = "N.A";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sGPSType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iGPS"></param>
        /// <returns></returns>
        public static string GetGPSType(int iGPS)
        {
            string sGPSType = "N.A";

            try
            {
                EGPSType eGPStype = (EGPSType)iGPS;
                switch (eGPStype)
                {
                    case EGPSType.eGPS_TYPE_RTC:
                        sGPSType = "RTC";
                        break;

                    case EGPSType.eGPS_TYPE_SPI:
                        sGPSType = "SPI";
                        break;

                    case EGPSType.eGPS_TYPE_PPS:
                        sGPSType = "PPS";
                        break;

                    default:
                        sGPSType = "N.A";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sGPSType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sFullFilename"></param>
        /// <returns></returns>
        public static FileDataFields SplitFullFilenames(string sFullFilename)
        {
            FileDataFields fileData = new FileDataFields();
            FileInfo fileInfo = new FileInfo(sFullFilename);
            string sDirectoryName = fileInfo.DirectoryName;

            fileData.fIsLogFile = false;
            try
            {
                fileData.sSize = fileInfo.Length.ToString();
            }
            catch { }
            {
            }

            fileData.sFilename = fileInfo.Name;

            SplitFilenames(ref fileData);

            m_fileData = fileData;
            return fileData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="fIsWavFile"></param>
        /// <param name="fIsLogFile"></param>
        public static void PharseFilename(ref FileDataFields fileData, bool fIsWavFile = false, bool fIsLogFile = false)
        {
            try
            {
                string[] sSplitFileName = fileData.sFilename.Split('.');
                string sFilename = sSplitFileName[0];
                sSplitFileName = sFilename.Split('_');
                if (sSplitFileName.Length > 7)
                {
                    string tFileSyncType = sSplitFileName[0];
                    bool bIsSyncRadio = (tFileSyncType == "TestR") || (tFileSyncType == "autoR");
                    fileData.sUnitName = sSplitFileName[1];
                    fileData.sTransmitTime = sSplitFileName[2] + "-" + sSplitFileName[3] + "-" + sSplitFileName[4] + "_" + sSplitFileName[5] + ":" + sSplitFileName[6] + ":" + sSplitFileName[7];
                    if (fIsWavFile == true)
                    {
                        fileData.sGPS = GetGPSType(sSplitFileName[8]);
                        fileData.sGainUsed = sSplitFileName[9];
                        fileData.nSyncDuration = (bIsSyncRadio)? int.Parse(sSplitFileName[10]) : 0;
                    }
                    else
                    {
                        fileData.sGPS = "N/A";
                        fileData.sGainUsed = "N/A";
                        fileData.nSyncDuration = 0;
                    }
                    fileData.fIsLogFile = fIsLogFile;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileData"></param>
        public static void SplitFilenames(ref FileDataFields fileData)
        {
            fileData.sUnitVersion = FIRMWARE_VERSION_UNKNOWN;

            try
            {
                do
                {
                    // wave file name format
                    //===================================
                    // auto_<Modem#>_<Day>_<Month>_<Year>_<Hour>_<Minutes>_<Seconds>_<GPS Sync Type>_<Gain>_<Pps Drift Test Time>_<Pps Ticks>_<Pps Record Time Difference>_<Min Noise Gain STD Calculated Gain * 10>_<STD Calculated Gain * 10>_sig.wav
                    // auto_354678051333768_22_02_2016_15_45_00_2_4_0_0_1962380_0_24_sig.wav

                    // Radio wave file name format
                    //===================================
                    // autoR_<Modem#>_<Day>_<Month>_<Year>_<Hour>_<Minutes>_<Seconds>_<GPS Sync Type>_<Gain>_<Radio Recording Time>_<Pps Drift Test Time>_<Pps Ticks>_<Pps Record Time Difference>_<Min Noise Gain STD Calculated Gain * 10>_<STD Calculated Gain * 10>_sig.wav
                    // autoR_354678051333768_22_02_2016_15_45_00_2_4_5_0_0_1962380_0_24_sig.wav

                    if ((fileData.sFilename.Contains(WAVE_FILE_NAME_EXTENSION) == true) &&
                        ((fileData.sFilename.Contains(WAVE_FILE_NAME_PREFIX) == true) ||
                         (fileData.sFilename.Contains(TEST_WAVE_FILE_NAME_PREFIX) == true) ||
                         (fileData.sFilename.Contains(RADIO_WAVE_FILE_NAME_PREFIX) == true) ||
                         (fileData.sFilename.Contains(RADIO_TEST_WAVE_FILE_NAME_PREFIX) == true)))
                    {
                        PharseFilename(ref fileData, true, false);
                        fileData.eFileType = EFileType.eWAV_FILE_TYPE;
                        break;
                    }

                    // old log file name format
                    // ========================
                    // auto_<Modem#>_<Day>_<Month>_<Year>_<Hour>_<Minutes>_<Seconds>_log.bindat
                    // auto_354678050252464_17_11_2015_15_24_00_log.bindat

                    if (((fileData.sFilename.Contains(LOG_FILE_NAME_OLD_EXTENSION) == true) &&
                         (fileData.sFilename.Contains(LOG_FILE_NAME_NEW_PREFIX) == true)) ||
                        ((fileData.sFilename.Contains(LOG_FILE_NAME_NEW_EXTENSION) == true) &&
                         ((fileData.sFilename.Contains(LOG_FILE_NAME_NEW_PREFIX) == true) ||
                          (fileData.sFilename.Contains(RADIO_LOG_FILE_NAME_NEW_PREFIX) == true) ||
                          (fileData.sFilename.Contains(TEST_LOG_FILE_NAME_NEW_PREFIX) == true) ||
                          (fileData.sFilename.Contains(RADIO_TEST_LOG_FILE_NAME_NEW_PREFIX) == true))))
                    {
                        PharseFilename(ref fileData, false, true);
                        fileData.eFileType = EFileType.eLOG_FILE_TYPE;
                        if ((fileData.sFilename.Contains(LOG_FILE_NAME_OLD_EXTENSION) == true) &&
                            (fileData.sFilename.Contains(LOG_FILE_NAME_NEW_PREFIX) == true))
                        {
                            fileData.eLogType = ELogFileType.eLOG_FILE_OLD_FORMAT;
                        }
                        else
                        {
                            fileData.eLogType = ELogFileType.eLOG_TYPE_NEW_FORMAT;
                        }
                        break;
                    }

                    // pressure sensor file name format
                    //===================================
                    // Trendp<Modem#>_<Day>_<Month>_<Year>_<Hour>_<Minutes>_<Seconds>_<miliseconds>.bin
                    // Trendp_354678051272263_22_02_2016_13_18_35_000.bin;

                    if ((fileData.sFilename.Contains(PRESSURE_SENSOR_FILE_NAME_EXTENSION) == true) &&
                        ((fileData.sFilename.Contains(PRESSURE_SENSOR_FILE_NAME_PREFIX) == true) ||
                         (fileData.sFilename.Contains(PRESSURE_SENSOR_HIT_FILE_NAME_PREFIX) == true)))
                    {
                        PharseFilename(ref fileData, false, false);
                        fileData.eFileType = EFileType.ePRESSURE_SENSOR_FILE_TYPE;
                        break;
                    }

                    // alarm file name format
                    //===================================
                    // Alarm_<Modem#>_<Day>_<Month>_<Year>_<Hour>_<Minutes>_<Seconds>_<miliseconds>_<Number of Hiting up to sendig the file>.txt
                    // Alarm_359658044010082_20_12_2015_23_34_30_000_6.txt

                    if ((fileData.sFilename.Contains(ALARM_FILE_NAME_EXTENSION) == true) &&
                        (fileData.sFilename.Contains(ALARM_FILE_NAME_PREFIXPREFIX) == true))
                    {
                        PharseFilename(ref fileData, false, false);
                        fileData.eFileType = EFileType.eALARM_FILE_TYPE;
                        break;
                    }

                    // config file name format
                    //===================================
                    // config_<Modem#>.bindat
                    // config_359658044010082.bindat

                    if ((fileData.sFilename.Contains(CONFIG_FILE_NAME_EXTENSION) == true) &&
                        (fileData.sFilename.Contains(CONFIG_FILE_NAME_PREFIX) == true))
                    {
                        PharseFilename(ref fileData, false, false);
                        fileData.eFileType = EFileType.eCONFIG_FILE_TYPE;
                        break;
                    }

                    // registration file name format
                    //===================================
                    // reg_<Modem#>.bindat
                    // reg_359658044010082.bindat

                    if ((fileData.sFilename.Contains(REGISTRATION_FILE_NAME_EXTENSION) == true) &&
                        (fileData.sFilename.Contains(REGISTRATION_FILE_NAME_PREFIX) == true))
                    {
                        PharseFilename(ref fileData, false, false);
                        fileData.eFileType = EFileType.eREGISTRATION_FILE_TYPE;
                        break;
                    }

                    // firmware file name format
                    //===================================
                    // firmware_<Minor>_<Major>.bin
                    // firmware_2_89.bin

                    if ((fileData.sFilename.Contains(FIRMWARE_FILE_NAME_EXTENSION) == true) &&
                        (fileData.sFilename.Contains(FIRMWARE_FILE_NAME_PREFIX) == true))
                    {
                        PharseFilename(ref fileData, false, false);
                        fileData.eFileType = EFileType.eFIRMWARE_FILE_TYPE;
                        break;
                    }

                    fileData.sDate = "";

                } while (false);
            }

            catch (Exception ex)
            {
                fileData.sDate = "";
                throw ex;
            }
        }

        /// <summary>
        /// Splite the FTP name as recived frol LIST FTP command
        /// Sample value is : "09-17-11  01:00AM               942038 my.zip"
        /// </summary>
        /// <param name="sFilename"></param>
        /// <returns></returns>
        public static FileDataFields SplitNames(string sFilename)
        {
            FileDataFields fileData = new FileDataFields();

            //DateTime dtDateCreated;
            string[] sarTokens = sFilename.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (sarTokens.Length > 3)
            {
                fileData.sDate = (DateTime.ParseExact(sarTokens[0] + sarTokens[1], "MM-dd-yyhh:mmtt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss");
                fileData.fIsLogFile = false;
                fileData.sSize = sarTokens[2];
                fileData.sFilename = sarTokens[3];
                SplitFilenames(ref fileData);
            }
            else
            {
                fileData.sDate = "";
            }
            m_fileData = fileData;
            return fileData;
        }

        /// <summary>
        /// Sample value is :
        ///     Microsoft FTP server return format
        ///     ============================================
        ///     "12-20-15  12:11PM               1107 BackupTest_354678051272560_20_12_2015_01_00_00.log"
        ///     
        ///     [0]	"12-20-15"	string
        ///     [1]	"12:11PM"	string
        ///     [2]	"1107"	string
        ///     [3]	"BackupTest_354678051272560_20_12_2015_01_00_00.log"	string
        /// 	Linux-Unix  FTP server return format
        /// 	====================================
        /// 	permissions	number?[tab]owner[tab]group[tab]filesize[tab]date[tab]filename 
        /// 	
        /// 	"-rw-r--r--    1 ftp      ftp          1942 Feb 15 15:21 BackupTest_354678051275589_15_02_2016_23_30_00.log"
        /// 
        ///     permissions	[0]	"-rw-r--r--"	string
        ///     number?		[1]	"1"	string
        ///     owner		[2]	"ftp"	string
        ///     group		[3]	"ftp"	string
        ///     filesize	[4]	"1942"	string
        ///     date		[5]	"Feb"	string  
        ///                 [6]	"15"	string
        ///                 [7]	"15:21"	string
        ///     filename 	[8]	"BackupTest_354678051275589_15_02_2016_23_30_00.log"	string
        /// 
        /////// </summary>
        /// <param name="sfileMsg"></param>
        /// <returns></returns>
        public static FileDataFields SplitFNames(string sfileMsg)
        {
            FileDataFields fileData = new FileDataFields();
            fileData.SetDefault();

            int iIndexFilesize = 2;
            int iIndexFilename = 3;
            int iIndexDate = 0;
            int iIndexTime = 1;

            try
            {
                //DateTime dtDateCreated;
                string[] sarTokens = sfileMsg.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (sarTokens.Length > 3)
                {
                    //dtDateCreated = DateTime.ParseExact(sarTokens[0] + sarTokens[1], "MM-dd-yyhh:mm", CultureInfo.InvariantCulture);
                    if (sarTokens.Length > 7) // Unix FTP LIST command retiun file list format
                    {
                        iIndexFilesize = 4;
                        iIndexFilename = 8;
                        iIndexDate = 5;
                        iIndexTime = 7;
                        // Parse date and time with custom specifier.
                        //dateString = "Sun 15 Jun 2008 8:30 AM -06:00";
                        //format = "ddd dd MMM yyyy h:mm tt zzz";
                        // "Feb""15""15:21" MMMddHH:tt"
                        string sDateUnix = sarTokens[iIndexDate + 1] + "-" + sarTokens[iIndexDate] + "-" + DateTime.UtcNow.Year.ToString() + " " + sarTokens[iIndexTime];
                        try
                        {
                            fileData.sDate = (DateTime.ParseExact(sDateUnix, "dd-MMM-yyyy HH:mm", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss");
                        }
                        catch
                        {
                            sDateUnix = sarTokens[iIndexDate + 1] + "-" + sarTokens[iIndexDate] + "-" + sarTokens[iIndexDate + 2] + " 01:01";
                            try
                            {
                                fileData.sDate = (DateTime.ParseExact(sDateUnix, "dd-MMM-yyyy HH:mm", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss");
                            }
                            catch (Exception ex1)
                            {
                                fileData.sDate = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss");
                                throw ex1;
                            }
                        }
                    }
                    else // Windows FTP LIST command retiun file list format
                    {
                        fileData.sDate = (DateTime.ParseExact(sarTokens[iIndexDate] + sarTokens[iIndexTime], "MM-dd-yyhh:mmtt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss");
                    }

                    fileData.sSize = sarTokens[iIndexFilesize];
                    fileData.sFilename = sarTokens[iIndexFilename];
                    fileData.fIsLogFile = false;
                    SplitFilenames(ref fileData);
                }
            }
            catch (Exception ex)
            {
                fileData.SetDefault();
            }

            m_fileData = fileData;
            return fileData;
        }

    }
}
