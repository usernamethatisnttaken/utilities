import pygame

from mech import Display
from mech import Fps

"""A simple boilerplate to save time building pygame mini-apps"""
class PygameBoilerplate:
    def __init__(self, title, dims = [1540, 865], fps = 20):
        self.fps = Fps(fps)
        self.display = Display(dims[0], dims[1], title)
        self.alive = True

    """Checks to see if the user has exited the program"""
    def Quit(self):
        for event in pygame.event.get():
            if event.type == pygame.QUIT: return True
        return False

    """The main executor of the class"""
    def Run(self):
        while not self.Quit():
            self.display.update()
            self.fps.buffer()
        self.alive = False
        pygame.quit()
        quit()

    def GetDisplay(self):
        return self.display.display
    
    def IsAlive(self):
        return self.alive

if(__name__ == "__main__"):
    PygameBoilerplate("").Run()