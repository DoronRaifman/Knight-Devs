
import numpy as np


def find_best_score_pairs(delta_i_j_orig: np.ndarray):
    delta_i_j = delta_i_j_orig.copy()
    matched_i_j = []
    for i in range(3):
        index = np.unravel_index(np.argmin(delta_i_j, axis=None), delta_i_j.shape)
        if delta_i_j[index] <= 10.0:
            matched_i_j.append(index)
            delta_i_j[index[0], :] = 1000
            delta_i_j[:, index[1]] = 1000
        else:
            break
    return matched_i_j


delta = np.array(
    [[2, 2, 3],
     [4, 5, 4],
     [7, 8, 1],
     ])

# 1 - (0, 0)
# 2 - (0,1), (1,0)

matched_i_j = find_best_score_pairs(delta)

print("delta")
print(delta)

print("matched_i_j")
print(matched_i_j)

print("best deltas")
best_deltas = [delta[index] for index in matched_i_j]
print(best_deltas)

