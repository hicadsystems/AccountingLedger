<template>
    	<!-- WRAPPER -->
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm" action="/BalanceSheet/CreateFinancial" method="post">
            <div class="card-body">
                <div class="row">
                    <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Stock Name</label>
                            <input type="text" name="stockname" class="form-control" v-model="postBody.stockname" required :readonly="submitorUpdate == 'Update'" />
                        </div>
                        </div>
                    <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Description</label>
                            <input class="form-control" name="description" v-model="postBody.description" placeholder="Description" />
                        </div>
                    </div>
                    <div class="col-12 ">
                        <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group">
                            <button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
							
	<!-- END WRAPPER -->
</template>

<script>
export default {
    
    data() {
        return {
            errors: null,
            responseMessage:'',
            submitorUpdate : 'Submit',
            canProcess : true,
            postBody: {
                stockname: '',
                description:''
            }
    
        }
    },
    watch:{
         '$store.state.objectToUpdate':function (newVal, oldVal) {
         this.postBody.stockname = this.$store.state.objectToUpdate.stockname,
         this.postBody.description = this.$store.state.objectToUpdate.description
         this.submitorUpdate = 'Update';
               
        }
    },
    methods: {
        checkForm: function (e) {
            
         if (this.postBody.stockname) {
              e.preventDefault();
              this.canProcess = false;
              this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('Balance Sheet Code is Required');
          }
        },
        postPost() {

                if(this.submitorUpdate == 'Submit'){
                    axios.post(`/api/Stock/createStock`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                this.postBody.stockname = ''; this.postBody.description = '';
                                this.$store.state.objectToUpdate = "create";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
                if(this.submitorUpdate == 'Update'){
                    axios.put(`/api/Stock/updateStock`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                 this.submitorUpdate = 'Submit'
                                this.postBody.stockname = ''; this.postBody.description = '';
                                this.$store.state.objectToUpdate = "update";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        },
        computed: {
            setter(){
                let objecttoedit = this.$store.state.objectToUpdate;
                if(objecttoedit.stockname){
                    this.postBody.stockname = objecttoedit.stockname;
                    this.postBody.description = objecttoedit.description;
                }
            }
        }
};
</script>