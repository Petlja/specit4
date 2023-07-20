import sys
sudoku = dict()
for line in sys.stdin:
    line = line.strip()
    try:
        x = int(line)
        if x == 0:
            continue
        x -= 1        
        i = x % 9
        j = (x // 9) % 9
        v = x // 81 + 1
        sudoku[(i, j)] = v
    except:
        pass

for i in range(9):
    for j in range(9):
        print(sudoku[(i, j)], end="")
    print()
