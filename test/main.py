from javax.swing import JFrame
from javax.swing import JPanel
from javax.swing import JButton
from java.awt import BorderLayout
from java.awt import FlowLayout
from java.awt import Dimension
from pymageA_UI.panel_pymageA import Panel_pymageA

class MainWindow():
    def initilialize_ui(self):
        self.initilialize_frame()
        self.create_center_panel()
        
    def initilialize_frame(self):
        self.frame = JFrame("PymageA",defaultCloseOperation= JFrame.EXIT_ON_CLOSE)
        main_panel =  self.frame.getContentPane()
        layout = BorderLayout()
        self.frame.setSize(Dimension(900,400))
        main_panel.setLayout(layout)


    def show_windows(self):
        self.frame.setVisible(True)

    def create_tool_bar(self):
        toolbar_panel=JPanel()
        toolbar_panel.setLayout(FlowLayout())
        toolbar_panel.add(JButton("Add Folder", actionPerformed =  self.add_folder_button_clicked))
        toolbar_panel.add(JButton("Find images"), actionPerformed = self.find_images_button_clicked)
        self.frame.getContentPane().add(toolbar_panel,BorderLayout.NORTH)

    def create_west_panel(self):
        self.west_panel =  ImageD()
        self.frame.getContentPane().add(self.west_panel,BorderLayout.EAST)


    def create_center_panel(self):
        center_panel = Panel_pymageA()
        self.frame.getContentPane().add(center_panel, BorderLayout.CENTER)




updated 3 lines
update 4 lines


if __name__ == "__main__":
    window = MainWindow()
    window.initilialize_ui()
    window.show_windows()
