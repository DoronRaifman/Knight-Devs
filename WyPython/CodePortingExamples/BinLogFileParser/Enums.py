"""
Author: Doron Raifman, draifman@gmail.com, Knight-Devs at https://knight-devs.com/
"""

from enum import Enum


class ELogRecordType(Enum):
    Illegal = -1
    EventRecord = 0
    StatusRecord = 1

    @classmethod
    def has_value(cls, value):
        return value in cls._value2member_map_


class ELogRecordDataType(Enum):
    NoValue = 0
    LogDataTypeByte = 1
    LogDataTypeShort = 2
    LogDataTypeUShort = 3
    LogDataTypeInt = 4
    LogDataTypeUInt = 5
    LogDataTypeFloat = 6
    LogDataTypeDouble = 7
    LogDataTypeBoolean = 8
    LogDataTypeChar = 9
    LogDataTypeString = 10
    LogDataTypeDateTime = 11
    # LogDataTypeLast = 12

    @classmethod
    def has_value(cls, value):
        return value in cls._value2member_map_


class StatusMessage:
    values = (
        "status_none",
        "status_date_time",
        "status_firmware_build",
        "status_firmware_version",
        "status_hw_version",
        "status_server_ip_used",
        "status_config_file_version",
        # config
        "status_record_time",
        "status_transmit_day",
        "status_start_record_time_1",
        "status_start_record_time_2",
        "status_start_record_time_3",
        "status_sample_rate",
        "status_gain",
        "status_gain_thresh_hold_1",
        "status_gain_thresh_hold_2",
        "status_gain_thresh_hold_3",
        "status_gps_wakeup_time_before_recording",
        "status_gain_wakeup_time_before_recording",
        "status_gain_sample_duration",
        "status_transmit_max_time",
        "status_transmit_window_start_time",
        "status_time_window_to_transmit",
        "status_time_pressure",
        "status_interval_pressure",
        "status_time_period_noise",
        "status_time_duration_noise",
        "status_noise_thresh_hold",
        # system info
        "status_gps_position_latitude",
        "status_gps_position_quadns",
        "status_gps_position_longitude",
        "status_gps_position_quadew",
        "status_gps_position_elevation",
        "status_gps_sync_type",
        "status_gps_start_rtc_time",
        "status_gps_end_rtc_time",
        "status_gps_time_of_satellite_locked",
        "status_gain_used",
        "status_gain_average_calculated",
        "status_gain_std_calculated",
        "status_start_time_recording_correction_vs_gps",
        "status_end_time_recording",
        "status_delta_time_between_gps_and_local",
        "status_last_sync_gps_pps_time",
        "status_sim_ccid",
        "status_gps_usage",
        "status_modem_usage",
        "status_bat_voltage_before_recording",
        "status_bat_voltage_after_recording",
        "status_temperature",
        # more options for sending ftp files
        "status_ftp_start_sending_file",
        "status_ftp_open_send_file_pass",
        "status_ftp_open_send_file_failed",
        "status_ftp_open_send_file_header",
        "status_ftp_send_file_size",
        "status_ftp_send_number_buffers",
        "status_ftp_send_number_buffers_zero_length",
        "status_ftp_send_faied_on_buffer_number",
        "status_ftp_finish_sending_file",
        "status_registraiton_file_name",
        "status_config_file_name",
        "status_last_reset_id_number",
        "status_last_system_reset_cause_by",
    )


class EventMessage:
    values = (
        "event_none",
        "event_system_start_up",
        "event_start_gain_calibration",
        "event_calculating_gain_calibration",
        "event_calculated_std_gain_calibration",
        "event_gain_is_fixed_value",
        "event_finished_gain_calibration",
        "event_set_gain_to_default_value",
        "event_finished_recording_wave_file",
        "event_start_sending_wave_files",
        "event_clearing_old_gain_calibration_data",
        "event_clearing_old_recording_data",
        "event_clearing_old_log_files",
        "event_opening_modem",
        "event_open_modem_failed",
        "event_close_modem_failed",
        "event_modem_opened_ok",
        "event_modem_closed_ok",
        "event_battery_too_low_for_modem",
        "event_closing_modem",
        "event_ftp_connected",
        "event_ftp_connect_failed",
        "event_start_sending_log_files",
        "event_start_gps_sync",
        "event_finished_gps_sync",
        "event_got_pps",
        "event_gps_sync_failed",
        "event_alarm_wakeup",
        "event_set_running_rtc_by_gps_sync",
        "event_modem_command_timeout",
        "event_gps_sync_spi_time",
        "event_setting_date_from_gps",
        "event_start_read_registraiton_data",
        "event_read_registraiton_data_ok",
        "event_read_registraiton_data_failed",
        "event_finish_read_registraiton_data",
        "event_start_read_default_registraiton_data",
        "event_read_default_registraiton_data_ok",
        "event_finish_read_default_registraiton_data",
        "event_read_default_registraiton_data_failed",
        "event_start_parsing_config_file",
        "event_finish_parsing_config_file",
        "event_start_parsing_registration_data",
        "event_error_parsing_registration_data",
        "event_finish_parsing_registration_data",
        "event_start_reading_config_file",
        "event_reading_config_file_ok",
        "event_reading_config_file_failed",
        "event_finish_reading_config_file",
        "event_modem_command_failed",
        "event_modem_signal_strength",
        "event_start_noise_gain_calibration",
        "event_radio_lock_channel_status",
        "event_radio_lock_channel_number",
        "event_radio_lock_channel_snr",
        "event_radio_modem_date_time_read",
        "event_finished_recording_radio_wave_file",
    )


class UnpackHelper:
    header_len = 10
    fields_description = \
        {
            # tuple: ({unpack format}, {offset}, {var size})
            'time_stamp': ('<L', 0, 4),
            'milli_sec': ('<H', 4, 2),
            'record_type': ('<B', 6, 1),
            'record_sub_type': ('<H', 7, 2),
            'record_data_type': ('<B', 9, 1),
        }
    unpack_format = {
        ELogRecordDataType.NoValue: (0, ""),
        ELogRecordDataType.LogDataTypeByte: (1, "<B"),
        ELogRecordDataType.LogDataTypeShort: (2, "<h"),
        ELogRecordDataType.LogDataTypeUShort: (2, "<H"),
        ELogRecordDataType.LogDataTypeInt: (4, "<i"),
        ELogRecordDataType.LogDataTypeUInt: (4, "<I"),
        ELogRecordDataType.LogDataTypeFloat: (4, "<f"),
        ELogRecordDataType.LogDataTypeDouble: (8, "<d"),
        ELogRecordDataType.LogDataTypeBoolean: (4, "<i"),
        ELogRecordDataType.LogDataTypeChar: (1, "<b"),
        ELogRecordDataType.LogDataTypeString: (50, "s"),
        ELogRecordDataType.LogDataTypeDateTime: (4, "<I"),
        # ELogRecordDataType.LogDataTypeLast: (2, "<"),
    }
