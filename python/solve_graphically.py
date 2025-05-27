import numpy as np
import matplotlib
import matplotlib.pyplot as plt
import sys

# Включаем интерактивный режим (только для обычного скрипта)
matplotlib.use('TkAgg')  # Убедись, что установлен tkinter

def calculate_line(a, b, c, x_range):
    if b != 0:
        y_vals = (c - a * x_range) / b
        return y_vals, None
    else:
        return None, c / a

def setup_axes():
    plt.title('Графічне рішення СЛАР')
    plt.xlabel('x')
    plt.ylabel('y')
    plt.axhline(0, color='black', linewidth=0.8)
    plt.axvline(0, color='black', linewidth=0.8)
    plt.grid(True, which='both', linestyle='--', linewidth=0.5)
    # убраны ограничения осей

def plot_system(a1, b1, c1, a2, b2, c2):
    x_vals = np.linspace(-100, 100, 1000)  # Широкий диапазон

    y1_vals, x1_const = calculate_line(a1, b1, c1, x_vals)
    y2_vals, x2_const = calculate_line(a2, b2, c2, x_vals)

    plt.figure(figsize=(8, 6))

    if y1_vals is not None:
        plt.plot(x_vals, y1_vals, label=f'{a1}x + {b1}y = {c1}')
    else:
        plt.axvline(x1_const, label=f'{a1}x + {b1}y = {c1}')

    if y2_vals is not None:
        plt.plot(x_vals, y2_vals, label=f'{a2}x + {b2}y = {c2}')
    else:
        plt.axvline(x2_const, label=f'{a2}x + {b2}y = {c2}')

    setup_axes()
    plt.legend()
    plt.tight_layout()
    plt.show()

def main():
    if len(sys.argv) != 7:
        sys.exit(1)
    try:
        a1, b1, c1, a2, b2, c2 = map(float, sys.argv[1:7])
        plot_system(a1, b1, c1, a2, b2, c2)
    except ValueError:
        print("Ошибка: все аргументы должны быть числами.")

if __name__ == '__main__':
    main()
