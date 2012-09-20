#!/usr/bin/env python
#GUI

def process(line):
    print line

def processFile(file):
    f = open(file)
    for line in f:
        process(line)
    f.close()

import wx
def load(event):
    processFile(filename.GetValue())

app = wx.App()
win = wx.Frame(None, title="Simple Editor")
loadBtn = wx.Button(win, label='Open', pos=(225, 5), size=(80, 25))
saveBtn = wx.Button(win, label='Save', pos=(315, 5), size=(80, 25))
filename = wx.TextCtrl(win, pos=(5, 5), size=(210, 25))
contents = wx.TextCtrl(win, pos=(5, 35), size=(395, 100),
                        style=wx.TE_MULTILINE | wx.HSCROLL)
win.Show()
app.MainLoop()
