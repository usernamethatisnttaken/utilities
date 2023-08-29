import pygame
import time

"""Immiediate dependencies for the visual "main" template file"""
"""Higher-level display info class"""
class Display():
    def __init__(self, x, y, title):
        self.display = pygame.display.set_mode((x, y))
        self.x = x
        self.y = y

        pygame.init()
        pygame.display.set_caption(title)

    def update(self):
        pygame.display.update()
        self.display.fill('white')

"""Used for maintaining the frame speed of the mini-app """
class Fps():
    def __init__(self, fps):
        self.length = 1 / fps
        self.start_time = time.perf_counter() - self.length

    def start(self):
        self.start_time = time.perf_counter()

    def buffer(self):
        end_time = time.perf_counter()
        while end_time < self.start_time + self.length:
            end_time = time.perf_counter()
        self.start()