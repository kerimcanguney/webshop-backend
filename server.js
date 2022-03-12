const express = require('express')
const app = express()
const mongoose = require('mongoose')

app.get("/", (req,res) =>{
    res.send("product list")
})

// routes
const productRouter = require('./routes/products')


// connect to DB
mongoose.connect(
    'mongodb://localhost:27017/products', 
    { useNewUrlParser: true}, 
    () => console.log('connect to db')
)
    

// enabling routes
app.use('/products',productRouter)


// 
app.listen(3000)

