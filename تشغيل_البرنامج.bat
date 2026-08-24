@echo off
chcp 65001 > nul
title تشغيل برنامج ميزان بسكول ابو السعد
cd /d "%~dp0"
echo جاري تشغيل برنامج ميزان الشاحنات...
start "" "Weighbridge.exe"
exit
