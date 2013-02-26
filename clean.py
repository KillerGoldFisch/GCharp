


import os
import time
import shutil
import re
import filecmp

root_dir =  os.path.dirname(os.path.abspath(__file__))

def SizeToString(datei):
    units = ["Byte","KiB","MiB","GiB","TiB"]
    inde = 0
    size = float(os.path.getsize(datei))
    while size > 1024:
        size /= 1024
        inde += 1
    return str(round(size,2))+units[inde]

for di, dirs, files in os.walk(root_dir):
    for fi in files:
        try:
            if re.findall("_conflict-[0-9]{8}-[0-9]{6}",fi):
                print "\n\n"
                copy = di+os.sep+fi
                orginal = di+os.sep+re.sub("_conflict-[0-9]{8}-[0-9]{6}","", fi)
                if not os.path.exists(orginal):
                    print "Unnoetiger Konflikt : "+orginal
                    shutil.move(copy, orginal)
                    continue
                
                print "In Konflikt stehende Datei: "+orginal
                print "Mit: " + copy
                
                create_date_c = time.gmtime(os.stat(copy)[9])
                print " Datum Copy : " + time.strftime("%Y-%m-%d", create_date_c)
                create_date_o = time.gmtime(os.stat(orginal)[9])
                print " Datum Orginal : " + time.strftime("%Y-%m-%d", create_date_o)
                print " Groesse Copy : "+SizeToString(copy)
                print " Groesse Orginal : "+SizeToString(orginal)
                if filecmp.cmp(orginal, copy):
                    print "Beide Dateien sind gleich, behalte 'Orginal'"
                    print "os.remove(%s)" % copy
                    os.remove(copy)
                    continue
                inp = raw_input("   Welche Datei Behalten? o=Orginal c=Copy *=Beide : ").lower()
                if inp == "o":
                    print "os.remove(%s)" % copy
                    os.remove(copy)
                elif inp == "c":
                    print "os.remove(%s)" % orginal
                    print "shutil.move(%s,%s)" % (copy, orginal)
                    os.remove(orginal)
                    shutil.move(copy,orginal)
                else:
                    print "*"
        except Exception as ex:
            print "ERROR : " + str(ex)
            
            