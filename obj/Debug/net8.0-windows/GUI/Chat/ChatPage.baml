   M S B A M L   `   `   `  ����  st10318880_poe1'    !ST10318880_POE1.GUI.Chat.ChatPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  ChatPage��5       N� 
Background 5      �� 5   
    �� ImageSource$3 -pack://application:,,,/Images/background2.jpg��6    j� Stretch$ UniformToFill=�5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   ���	#CCFFFFFF �� CornerRadius$ 12��5       �� Padding$ 10q�5      ���#ccc$��1q�5      $	��750��5      $��0,0,0,10q�5      ._�5      �� -   5         ��Name$ ChatScrollViewer��5      $
��Auto=�5      .��5      �� -   5         ��Name$ 
ChatOutput��5      $
��Wrap=�5      $��14(�5      5      5      �� 5"   
   $��1��6   	 �� Orientation$	 
Horizontal=�5#      $��0,10,0,0q�5$      .+�5%      �� -   5        
 ��Name$
 	UserInput��5%      $	��400��5&      $��0,0,10,0q�5'      5(      �� 5)      -   5        $
��Send��5)      $��80��5*      5+      5,      5-      5.      