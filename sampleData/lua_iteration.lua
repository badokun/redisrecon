local sum = 0
local matches = redis.call('smembers', 'urn:battle:p:0:Left')

for _,key in ipairs(matches) do
    --local val = redis.call('GET', key)
    sum = sum + 1
end

return sum