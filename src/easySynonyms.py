print('''
        注:使用本脚本前先确保安装了synonyms 库  (安装方法 : pip install -U synonyms -i https://mirrors.ustc.edu.cn/pypi/web/simple/ )
''')

import os
import synonyms

print('''

┌─┐┌─┐┌─┐┬ ┬  ┌─┐┬ ┬┌┐┌┌─┐┌┐┌┬ ┬┌┬┐┌─┐
├┤ ├─┤└─┐└┬┘  └─┐└┬┘││││ ││││└┬┘│││└─┐
└─┘┴ ┴└─┘ ┴   └─┘ ┴ ┘└┘└─┘┘└┘ ┴ ┴ ┴└─┘

''')


print('''
        使用说明:
            近义词   :词句
            语句比较 :输入第一句 vs 输入第二句
            语句拆分 :seg 词句
''')

compareTag = ' vs '
segTag = 'seg '

# 近义词 打印
def cPrintDisplay(str):
    print ('@+@')
    synonyms.display(str)
    print ('@-@')

def cPrintCompare(s1,s2):
    result = synonyms.compare(s1,s2)
    print ('@+@')
    print(s2 , compareTag , s1 , '近似度:' , result)
    print ('@-@')

def cPrintSeg(str):
    result = synonyms.seg(str)
    print ('@+@')
    print('拆分为:' , result)
    print ('@-@')

# 命令解析
def commandParse(str):
    if str.find(compareTag) != -1:
        # print('compareTag')
        strs = str.split(compareTag)
        cPrintCompare(strs[0] , strs[1])
    elif str.find(segTag) != -1:
        strs = str.split(segTag)
        cPrintSeg(strs[1])
    else:
        cPrintDisplay(str)
        # print('display')

def loop(str):
    if str != '':
        commandParse(str)
    nextc = input()
    loop(nextc)

loop("")