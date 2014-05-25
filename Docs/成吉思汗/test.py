d = {1:'A',2:'A',3:'B',4:'D'}

print d
print sum(1 for x in d if x == 1 and d[x] == 'A')

l = (x in d if x>3)

