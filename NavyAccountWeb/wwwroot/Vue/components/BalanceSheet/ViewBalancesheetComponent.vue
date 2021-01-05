<template>
    	<!-- WRAPPER -->
    <div>
        
        <div class="card-body">
            <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Code</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="balanceSheet in balanceSheetList">
                        <td>{{ balanceSheet.bl_code }}</td>
                        <td>{{ balanceSheet.bl_desc }}</td>
                       
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(balanceSheet)">Edit</button></td>
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processDelete(balanceSheet.id)">Delete</button></td>
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
                    balanceSheetList: null,
                    responseMessage:''
                };
            },
        created() {
            this.$store.state.objectToUpdate = null;
        },
    watch:{
         '$store.state.objectToUpdate':function (newVal, oldVal) {
            this.getAllBalanceSheets();
            this.processDelete();
        }
    },
    mounted () {
        this.getAllBalanceSheets();
        this.processDelete();
     },
     methods: {
 
        processRetrieve : function (balanceSheet) {
            this.$store.state.objectToUpdate = balanceSheet;
         },
         processDelete: function (id) {

             axios.get(`/api/BalanceSheet/RemoveBalsheet/${id}`)
                 .then(response => {
                     if (response.data.responseCode == '200') {
                         alert("balance sheet successfully deleted");
                         this.getAllBalanceSheets();
                     }
                 }).catch(e => {
                            this.errors.push(e)
                        });

         },
         getAllBalanceSheets: function () {
             axios
            .get('/api/BalanceSheet/getAllBalanceSheets')
            .then(response => (this.balanceSheetList = response.data))
         }
    }
    
};
</script>