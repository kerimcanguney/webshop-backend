const express = require('express')
const app = express()
const mongoose = require('mongoose')
require('dotenv/config')

// routes
const productRouter = require('./routes/products')

// connect to DB
mongoose.connect(
    process.env.DB_CONNECTION, 
    { useNewUrlParser: true}, 
    () => console.log('connect to db')
)
    
// enabling routes
app.use('/products',productRouter)


// server port
app.listen(3000)

