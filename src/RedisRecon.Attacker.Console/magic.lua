local sum = 0
local matches = redis.call('smembers', '"urn:battle:p:0:Left"')

for _,key in ipairs(matches) do
    --local val = redis.call('GET', key)
    sum = sum + 1
end

return sum



local collect = {}
local match_pattern = "*"
local results = redis.call("SSCAN", "urn:battle:0:rightOnlyOrDiff", 0, "match", match_pattern)
for i, key_name in ipairs(results[2]) do 
  -- your code here (could be different depending on your needs)
  local key_value = redis.call("GET", "data_type:" .. key_name)
  if key_value then
    table.insert(collect, key_value)
  end 
end 
return collect


local members = redis.pcall('smembers','urn:battle:0:Both')

for i=1,table.getN(members) do

 local value = members[i]
  -- do some logic on the value
  local left = redis.call("GET", "urn:b:0:Left:" + value)
  local right = redis.call("GET", "urn:b:0:Right:" + value)

  -- urn:b:0:Left:132716
end