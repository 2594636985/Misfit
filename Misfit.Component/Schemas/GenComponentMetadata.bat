@echo off
set sdkDir="C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\"

%sdkDir%\bin\xsd.exe ComponentMetadata.xsd /classes /l:cs /n:Egeye.Component.Metadata /o:..\Metadata

@echo on
