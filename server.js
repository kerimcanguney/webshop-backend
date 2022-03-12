const express = require('express')
const app = express()
 
app.get("/", (req,res) =>{
    res.send('empty')
})

const productRouter = require('./routes/products')

app.use('/products',productRouter)

app.listen(3000)

