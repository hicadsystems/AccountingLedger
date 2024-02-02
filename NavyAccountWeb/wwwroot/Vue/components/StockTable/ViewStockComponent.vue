<template>
    	<!-- WRAPPER -->
    <div>
        
        <div class="card-body">
            <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="stock in stockList">
                        <td>{{ stock.stockname }}</td>
                        <td>{{ stock.description }}</td>
                       
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(stock)">Edit</button></td>
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processDelete(stock.id)">Delete</button></td>
                    </tr>
                </tbody>
              
            </table>
    </div>

    </div>

        <!-- END WRAPPER -->
</template>

<script>
export default { 
    
            data() {
                return {
                    stockList: null,
                    responseMessage:''
                };
            },
        created() {
            this.$store.state.objectToUpdate = null;
        },
    watch:{
         '$store.state.objectToUpdate':function (newVal, oldVal) {
            this.getAllStocks();
            this.processDelete();
        }
    },
    mounted () {
        this.getAllStocks();
        this.processDelete();
     },
     methods: {
 
        processRetrieve : function (stock) {
            this.$store.state.objectToUpdate = stock;
         },
         processDelete: function (id) {

             axios.get(`/api/Stock/RemoveStock/${id}`)
                 .then(response => {
                     if (response.data.responseCode == '200') {
                         alert("Stock successfully deleted");
                         this.getAllBalanceSheets();
                     }
                 }).catch(e => {
                            this.errors.push(e)
                        });

         },
         getAllStocks: function () {
             axios
            .get('/api/Stock/getAllStocks')
            .then(response => (this.stockList = response.data))
         }
    }
    
};
</script>