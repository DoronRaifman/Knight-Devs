"""
Author: Doron Raifman, draifman@gmail.com, Knight-Devs at https://knight-devs.com/
"""

from datetime import datetime, timedelta


class EpocDate:
    @classmethod
    def epoc_datetime_to_datetime(cls, cur_datetime: int):
        ep = datetime(1970, 1, 1, 0, 0, 0)
        res = ep + timedelta(seconds=cur_datetime)
        # TODO: 1 handle utc
        return res

    @classmethod
    def datetime_to_epoc_datetime(cls, cur_datetime: datetime):
        ep = datetime(1970, 1, 1, 0, 0, 0)
        delta = (cur_datetime - ep).total_seconds()
        # TODO: 1 handle utc
        return int(delta)

