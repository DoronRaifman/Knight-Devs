"""
KeyboardInterface
Multi platform keyboard interface
Author: Raifman Doron, draifman@gmail.com

Note: you should call init and cleanup
This will not work from inside pycharm
"""

import os
import sys
import time


class KeyboardInterface:
    if os.name != 'nt':
        import termios
        fd = sys.stdin.fileno()
        old_term = termios.tcgetattr(fd)
        new_term = termios.tcgetattr(fd)
        new_term[3] = (new_term[3] & ~termios.ICANON & ~termios.ECHO)

    @classmethod
    def init(cls):
        if os.name != 'nt':
            import termios
            import atexit
            termios.tcsetattr(cls.fd, termios.TCSAFLUSH, cls.new_term)
            atexit.register(cls.cleanup())

    @classmethod
    def cleanup(cls):
        if os.name != 'nt':
            import termios
            termios.tcsetattr(cls.fd, termios.TCSAFLUSH, cls.old_term)
            import atexit
            atexit.unregister(cls.cleanup())

    @classmethod
    def kbhit(cls):
        is_kbhit = False
        if os.name == 'nt':
            import msvcrt
            if msvcrt.kbhit():
                is_kbhit = True
        else:
            from select import select
            dr, dw, de = select([sys.stdin], [], [], 0)
            if dr:
                is_kbhit = True
        return is_kbhit

    @classmethod
    def getch(cls):
        if os.name == 'nt':
            import msvcrt
            c = msvcrt.getch()
        else:
            c = sys.stdin.read(1)
        return c

    @classmethod
    def putch(cls, c):
        if os.name == 'nt':
            import msvcrt
            msvcrt.putch(c)
        else:
            sys.stdout.write(c)


if __name__ == '__main__':
    print("Testing KeyboardInterface. Hit any key to terminate")
    KeyboardInterface.init()
    while not KeyboardInterface.kbhit():
        time.sleep(1)
    c = KeyboardInterface.getch()
    print(f"KeyboardInterface: Key {c} pressed")
    KeyboardInterface.cleanup()


