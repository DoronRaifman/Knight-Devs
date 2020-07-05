import numpy as np
import matplotlib.pyplot as plt
from scipy.optimize import fsolve


def find_perpendicular_cross_point(line_point1, line_point2, point):
    x = [line_point1[0], line_point2[0]]
    y = [line_point1[1], line_point2[1]]
    # Calculate the coefficients
    coefficients = np.polyfit(x, y, 1)
    a = coefficients[0]
    b = coefficients[1]
    # Print the findings
    print(f'a={a}, b={b}')
    # diagonal line has new_a = -a
    # y = new_a * X + new_b
    # 1 = new_a * 2 + new_b
    # new_b = 1 - 2 * new_a
    new_a = -a
    new_b = point[1] - point[0] * new_a
    new_coefficients = (new_a, new_b)
    # solve equations
    mat_a = np.array([[1, -a],[1, -new_a]])
    mat_b = np.array([b, new_b])
    result = np.linalg.solve(mat_a, mat_b)
    print(f'result: {result}')
    res_x = result[1]
    res_y = result[0]
    return coefficients, new_coefficients, res_x, res_y


def find_shortest_line_point(line_point1, line_point2, point, coefficients, new_coefficients, res_x, res_y):
    min_line_x = min(line_point1[0], line_point2[0])
    min_line_y = min(line_point1[1], line_point2[1])
    max_line_x = max(line_point1[0], line_point2[0])
    max_line_y = max(line_point1[1], line_point2[1])
    if res_x >= min_line_x and res_y >= min_line_y:
        if res_x <= max_line_x and res_y <= max_line_y:
            # inside line
            result_point = [res_x, res_y]
        else:
            # less then min
            result_point = [max_line_x, max_line_y]
    else:
        # more then min
        result_point = [min_line_x, min_line_y]
    return result_point


# Define the known points
# Vertexes (0, 2), (2,2)
line_point1 = [1, 1]    # (x, y)
line_point2 = [5, 5]    # (x, y)
point = (6, 7)  # x, y

coefficients, new_coefficients, res_x, res_y = find_perpendicular_cross_point(line_point1, line_point2, point)
result_point = find_shortest_line_point(line_point1, line_point2, point, coefficients, new_coefficients, res_x, res_y)

# draw results

# Let's compute the values of the line...
x_axis = np.linspace(0, 10, 10)
polynomial = np.poly1d(coefficients)
y_axis = polynomial(x_axis)
new_polynomial = np.poly1d(new_coefficients)
y_axis_new = new_polynomial(x_axis)

plt.plot(x_axis, y_axis)
plt.plot(x_axis, y_axis_new)

plt.plot(point[0], point[1], 'go', color='green', label='point')
plt.plot(line_point1[0], line_point1[1], 'go', color='blue', label='Line')
plt.plot(line_point2[0], line_point2[1], 'go', color='blue')

plt.plot(res_x, res_y, 'go', color='red', label='result')
plt.plot(result_point[0], result_point[1], 'go', color='magenta', label='result point')

plt.legend(loc='upper center')
plt.grid('on')
plt.show()

