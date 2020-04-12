
import numpy as np
import matplotlib.pyplot as plt


def speed_for_dist(factor: float):
    max_speed = 100.0
    distance = np.linspace(0.0, 5.0, 100)
    speeds = max_speed * np.power(2.7, -factor * distance)
    return distance, speeds


fig = plt.figure(figsize=(20, 10))
plt.suptitle("Motor speed vs distance:\nSpeed = MaxSpeed * pow(e, -Factor * Distance)", size=30)
plt.xlabel("Distance [m]", size=20)
plt.ylabel("Speed [%]", size=20)
for factor in (0.1, 0.2, 0.3, 0.4, 0.5, 0.7, 1.0):
    distance, speeds = speed_for_dist(factor)
    plt.plot(distance, speeds, label=f'Factor={factor}')
plt.legend(loc='upper right', prop={'size': 20}, markerscale=6)
# plt.show()
plt.savefig("Result.png")
plt.close(fig)



