Select m.timestamp, m.typeid, mt.unit, mt.description, m.measurevalue
from Measurement m, Measurement_Type mt
where m.installationid = 0 and mt.typeid = m.typeid
group by m.typeid, m.timestamp, mt.unit, mt.description, m.measurevalue
having m.timestamp = (Select MAX(meas.timestamp) from Measurement meas where typeid = m.typeid);
