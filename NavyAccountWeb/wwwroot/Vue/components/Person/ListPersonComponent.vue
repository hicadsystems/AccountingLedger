<template>
    	<!-- WRAPPER -->
    <div>
        <div class="card-body">
         <div class="row">
            <div class="col-12 col-xl-4">
                    <div class="form-group">
                        <label class="form-label">Service Number</label>
                        <vuejsAutocomplete source="/api/PersonAPI/getAllPersonsByServiceNoLimited/"
                                           input-class="form-control"
                                           @selected="setValuePersonID"
                                           v-model="pp">
                        </vuejsAutocomplete>

                    </div>
                </div>
            </div>
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Service Number</th>
                        <th>Full Name</th>
                        <th>Rank </th>
                        <th>Gender</th>
                        <th>Account No</th>
                        <!--<th>Beneficiary</th>-->
                        <th>Update</th>
                    </tr>
                </thead>
                <tbody>
                 
                    <tr v-for="personel in personelList">
                        <td>{{ personel.svC_NO }}</td>
                        <td>{{ personel.firstName }}  {{ personel.middleName }}</td>
                        <td>{{ personel.rank }}</td>
                        <td>{{ getAppropriateGender(personel.sex) }}</td>
                        <td>{{ personel.accountno }}</td>
                        <!--<td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(mainact)">Beneficiary</button></td>-->
                        <td ><a type="button"  :href="'CreatePerson?id='+personel.personID" class="btn btn-submit btn-primary">Edit</a></td>
                    </tr>
                </tbody>

            </table>
            <paginate
               
                :page-count="getPageCount"
                :page-range="3"
                :margin-pages="2"
                :click-handler="clickCallback"
                :prev-text="'Prev'"
                :next-text="'Next'"
                class="pagination"
                :container-class="'pagination'"
                :page-class="'page-item'">
            </paginate>
        </div>
        

    </div>

        <!-- END WRAPPER -->
</template>

<script>
import Paginate from 'vuejs-paginate'
 import vuejsAutocomplete from 'vuejs-auto-complete'
export default {
    components:{
        Paginate,
         vuejsAutocomplete
    },
    data() {
        return {
        personelList:null,
        pageno:0,
        totalcount:0,
          PersonID:0,
         pp:''
        };
    },
     created() {
            this.$store.state.objectToUpdate = null;
    },
    computed: {
        getPageCount :function() {
            return ( Math.ceil(this.totalcount/10) - 0);
        }
    },
    mounted () {
        axios
        .get(`/api/PersonAPI/getAllPersons?pageno=${this.pageno}`)
        .then(response => {
            this.personelList =  response.data.personlist;
            this.totalcount = response.data.total;
        })
     },
       
     methods: {
         clickCallback: function (pageNum) {
             this.pageno = pageNum;
             axios.get(`/api/PersonAPI/getAllPersons?pageno=${this.pageno}`)
            .then(response => {
                
                this.personelList = response.data.personlist;
                this.totalcount = response.data.total;
            })
         },
          setValuePersonID: function(result) {
              alert(result.value)
             axios
           .get(`/api/PersonAPI/getPersonByID2/${result.value}`)
           .then(response => {this.personelList = response.data;
          
           })
        },
  
         processRetrieve: function (mainAccount) {

            //this.$store.state.objectToUpdate = mainAccount;
         },

         getAppropriateGender: function (gender) {
             if (gender == 'M')
                 return 'Male';
             if (gender == 'F')
                 return 'Female';
         }
    }
    
};
</script>
<style lang="css">

.pagination{display:inline-block;padding-left:0;margin:20px 0;border-radius:4px}.pagination>li{display:inline}.pagination>li>a,.pagination>li>span{position:relative;float:left;padding:6px 12px;margin-left:-1px;line-height:1.42857143;color:#337ab7;text-decoration:none;background-color:#fff;border:1px solid #ddd}.pagination>li>a:focus,.pagination>li>a:hover,.pagination>li>span:focus,.pagination>li>span:hover{z-index:2;color:#23527c;background-color:#eee;border-color:#ddd}.pagination>li:first-child>a,.pagination>li:first-child>span{margin-left:0;border-top-left-radius:4px;border-bottom-left-radius:4px}.pagination>li:last-child>a,.pagination>li:last-child>span{border-top-right-radius:4px;border-bottom-right-radius:4px}.pagination>.active>a,.pagination>.active>a:focus,.pagination>.active>a:hover,.pagination>.active>span,.pagination>.active>span:focus,.pagination>.active>span:hover{z-index:3;color:#fff;cursor:default;background-color:#337ab7;border-color:#337ab7}.pagination>.disabled>a,.pagination>.disabled>a:focus,.pagination>.disabled>a:hover,.pagination>.disabled>span,.pagination>.disabled>span:focus,.pagination>.disabled>span:hover{color:#777;cursor:not-allowed;background-color:#fff;border-color:#ddd}.pagination-lg>li>a,.pagination-lg>li>span{padding:10px 16px;font-size:18px;line-height:1.3333333}.pagination-lg>li:first-child>a,.pagination-lg>li:first-child>span{border-top-left-radius:6px;border-bottom-left-radius:6px}.pagination-lg>li:last-child>a,.pagination-lg>li:last-child>span{border-top-right-radius:6px;border-bottom-right-radius:6px}.pagination-sm>li>a,.pagination-sm>li>span{padding:5px 10px;font-size:12px;line-height:1.5}.pagination-sm>li:first-child>a,.pagination-sm>li:first-child>span{border-top-left-radius:3px;border-bottom-left-radius:3px}.pagination-sm>li:last-child>a,.pagination-sm>li:last-child>span{border-top-right-radius:3px;border-bottom-right-radius:3px}.pager{padding-left:0;margin:20px 0;text-align:center;list-style:none}.pager li{display:inline}.pager li>a,.pager li>span{display:inline-block;padding:5px 14px;background-color:#fff;border:1px solid #ddd;border-radius:15px}.pager li>a:focus,.pager li>a:hover{text-decoration:none;background-color:#eee}.pager .next>a,.pager .next>span{float:right}.pager .previous>a,.pager .previous>span{float:left}.pager .disabled>a,.pager .disabled>a:focus,.pager .disabled>a:hover,.pager .disabled>span{color:#777;cursor:not-allowed;background-color:#fff}

</style> 