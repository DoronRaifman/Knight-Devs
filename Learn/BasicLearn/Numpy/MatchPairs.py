import numpy as np


def match(vector1, vector2, threshold):
    delta_i_j = np.full((len(vector1), len(vector2)), threshold * 10, dtype=float)
    for i in range(len(vector1)):
        for j in range(len(vector2)):
            delta_i_j[i, j] = abs(vector1[i] - vector2[j])
    pair_list = find_best_score_pairs(delta_i_j, threshold)
    return pair_list


def find_best_score_pairs(delta_i_j_orig: np.ndarray, threshold):
    delta_i_j = delta_i_j_orig.copy()
    matched_i_j = []
    for i in range(delta_i_j.shape[1]):
        index = np.unravel_index(np.argmin(delta_i_j, axis=None), delta_i_j.shape)
        if delta_i_j[index] <= threshold:
            matched_i_j.append(index)
            delta_i_j[index[0], :] = threshold * 10
            delta_i_j[:, index[1]] = threshold * 10
        else:
            break
    return matched_i_j


if __name__ == '__main__':
    pair_list = match([120, 60, 20, ], [19, 57, 118,], 10)
    print(pair_list)



