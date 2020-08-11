
import numpy as np
from scipy import signal
import matplotlib.pyplot as plt


class TestDetrend:
    def __init__(self):
        pass

    @staticmethod
    def test_detrend():
        vector_sin = np.sin(np.linspace(0, 50, num=500))
        vector_slope = np.linspace(0, 2, num=500)
        vector = vector_sin + vector_slope
        vector_detrended = signal.detrend(vector)

        fig = plt.figure(figsize=(18, 10))
        plt.suptitle("Demonstrate detrend", size=30)
        plt.xlabel("time", size=20)
        plt.ylabel("value", size=20)
        plt.plot(vector, label=f'Original Signal', color='blue')
        plt.plot(vector_detrended, label=f'Detrended Signal', color='green')
        plt.legend(loc='upper center', prop={'size': 20}, markerscale=6)
        plt.grid()
        plt.show()
        # plt.savefig("Result.png")
        plt.close(fig)


if __name__ == '__main__':
    TestDetrend.test_detrend()

